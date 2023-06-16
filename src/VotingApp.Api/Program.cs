using Microsoft.AspNetCore.Mvc;
using VotingApp.Contract;
using VotingApp.Contract.Requests;
using VotingApp.Contract.Responses;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;
using VotingApp.Domain.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/workItems", (CreateWorkItemRequest request, IVotingService votingServivce) =>
{
    var host = new Participant(request.Host.Id, request.Host.Name);

    var workItem = votingServivce.CreateWorkItem(host, request.Names, request.IsAnonymous);

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

app.MapPut("/api/workItems/{id}:vote", ([FromRoute] string id, VotingRequest request, IVotingService votingService) =>
{
    var vote = new Vote(Guid.NewGuid().ToString(), request.Participant, id, request.Value);
    votingService.Vote(vote);
    return Results.Ok();
});

app.MapGet("/app/workItems/{id}", ([FromRoute] string id, IVotingService votingService) =>
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

    var response = new GetWorkItemResponse
    {
        Id = workItem.Id,
        Host = hostDto,
        Name = workItem.Name,
        IsAnonymous = workItem.IsAnonymous,
        VotingEnabled = workItem.VotingEnabled,
        Participants = workItem.Participants.Select(p => new ParticipantDto
        {
            Id = p.Id,
            Name = p.Name
        }),

        Votes = workItem.Votes.Select(v => new VoteDto
        {
            Id = v.Value.Id,
            Participant = new ParticipantDto
            {
                Id = v.Value.ParticipantId,
                Name = workItem.Participants.Find(p => p.Id == v.Value.ParticipantId)?.Name ?? ""
            },
            Value = workItem.VotingEnabled ? v.Value.Value : -1,
        }),
        
    };
    return Results.Ok(response);
});

app.MapPost("/api/workItems/{id}:enableVoting", ([FromRoute] string id, EnableVotingRequest request, IVotingService votingServivce) =>
{
    votingServivce.EnableVoting(id, request.HostId);
    return Results.Ok();
});

app.MapPost("/api/workItems/{id}:disableVoting", ([FromRoute] string id, DisableVotingRequest request, IVotingService votingServivce) =>
{
    votingServivce.DisableVoting(id, request.HostId);
    return Results.Ok();
});

app.Run();

