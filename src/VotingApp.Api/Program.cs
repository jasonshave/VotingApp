using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VotingApp.Contract;
using VotingApp.Contract.Requests;
using VotingApp.Contract.Responses;
using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;
using VotingApp.Domain.Service;
using VotingApp.Infrastructure.InMemoryDatabase;
using VotingApp.Persistence;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ICrudRepository<WorkItem>, WorkItemRepository>();
builder.Services.AddSingleton<IWorkItemRepository, AnotherWorkItemRepository>();
builder.Services.AddSingleton<IVotingService, VotingService>();
var app = builder.Build();
app.UseExceptionHandler(exceptionHandler =>
{
    exceptionHandler.Run(async context =>
    {

        // using static System.Net.Mime.MediaTypeNames;
        context.Response.ContentType = Text.Plain;

        var exceptionHandlerPathFeature =
            context.Features.Get<IExceptionHandlerPathFeature>();


        if (exceptionHandlerPathFeature?.Error is VotingApp.Domain.Abstractions.NotFoundException
        || exceptionHandlerPathFeature?.Error is VotingApp.Infrastructure.InMemoryDatabase.NotFoundException)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        else if (exceptionHandlerPathFeature?.Error is ForbiddenException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        }
        
        await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
    });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/workItems", async (CreateWorkItemRequest request, IVotingService votingServivce) =>
{
    var host = new Participant()
    {
        Id = request.Host.Id,
        Name = request.Host.Name
    };

    var workItem = votingServivce.CreateWorkItem(host, request.Name, request.IsAnonymous);

    var hostDto = new ParticipantDto
    {
        Id = workItem.Host.Id,
        Name = workItem.Host.Name
    };
    var response = new CreateWorkItemResponse
    {
        Id = workItem.Id,
        Host = hostDto,
        Name = workItem.Name,
        IsAnonymous = workItem.IsAnonymous,
        VotingEnabled = workItem.VotingEnabled,
        //Participants = workItem.Participants.Select(p => new ParticipantDto {
        //    Id = p.Id,
        //    Name = p.Name }),
    };
    return Results.Ok(response);
});

app.MapPut("/api/workItems/{id}:vote", async ([FromRoute] string id, VotingRequest request, IVotingService votingService) =>
{
    var participant = new Participant() 
    { 
        Id = request.ParticipantId
    };
    var vote = new Vote(Guid.NewGuid().ToString(), participant, id, request.Value);
    votingService.Vote(vote);
    return Results.Ok();
});

app.MapGet("/api/workItems/{id}/Voted", async ([FromRoute] string id, IVotingService votingService) =>
{
    var workItem = votingService.GetWorkItem(id);
    if (workItem == null)
    {
        return Results.NotFound($"WorkItem {id} is not found");
    }
    var hostDto = new ParticipantDto
    {
        Id = workItem.Host.Id,
        Name = workItem.Host.Name
    };

    var response = new GetVotedResponse
    {
        VoteCount = workItem.Votes.Count(),
        Participants = workItem.Votes.Select(p => new ParticipantDto
        {
            Id = p.Value.Participant.Id,
        })
    };
    return Results.Ok(response);
});

app.MapPost("/api/workItems/{id}:closeVoting", async ([FromRoute] string id, DisableVotingRequest request, IVotingService votingServivce) =>
{
    votingServivce.DisableVoting(id, request.HostId);
    var workItem = votingServivce.GetWorkItem(id);
    var response = new CloseVotingResponse
    {
        Name = workItem.Name,
        Votes = workItem.Votes.Select(p => new VoteDto
        {
            ParticipantId = p.Key,
            Value = p.Value.Value
        })
    };
    return Results.Ok(response);
});

app.Run();

