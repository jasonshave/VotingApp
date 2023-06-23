using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using VotingApp.TestHost;
using Xunit.Abstractions;

namespace VotingApp.Domain.Tests;
public class E2eTests : GenericTestHost
{
    public E2eTests(ITestOutputHelper testOutputHelper)
        : base(testOutputHelper)
    {
    }
    [Fact(DisplayName = "test create workitem and vote on it")]
    public void Participant_Should_Be_Able_To_Vote()
    {
        // arrange
        var votingService = _host.Services.GetRequiredService<IVotingService>();
        var hostParticipant = new Participant{
            Id = "hostId", 
            Name = "host" 
        };        
        
        // act
        var workItem = votingService.CreateWorkItem(hostParticipant, "workitem", false);
        var vote1 = new Vote("voteId1", hostParticipant, workItem.Id, 1);
        

        votingService.Vote(vote1);
        votingService.DisableVoting(workItem.Id, hostParticipant.Id);
        var savedWorkItem = votingService.GetWorkItem(workItem.Id);

        // assert
        savedWorkItem.Should().NotBeNull();
        savedWorkItem.Votes.First().Value.Should().BeSameAs(vote1);
        savedWorkItem.Votes.First().Key.Should().BeSameAs(hostParticipant.Id);
    }

    [Fact(DisplayName = "test get workItem Votes is orderby voting value")]
    public void Get_WorkItem_Vote_Should_InOrder()
    {
        // arrange
        var votingService = _host.Services.GetRequiredService<IVotingService>();
        var hostParticipant = new Participant
        {
            Id = "hostId",
            Name = "host"
        };

        var ParticipantZ = new Participant
        {
            Id = "zhostId",
            Name = "zParticipant"
        };

        var ParticipantA = new Participant
        {
            Id = "ahostId",
            Name = "aParticipant"
        };

        // act
        var workItem = votingService.CreateWorkItem(hostParticipant, "workitem", false);
        var vote1 = new Vote("voteId1", hostParticipant, workItem.Id, 3);
        var vote2 = new Vote("voteId2", ParticipantA, workItem.Id, 8);
        var vote3 = new Vote("voteId3", ParticipantZ, workItem.Id, 1);



        votingService.Vote(vote1);
        votingService.Vote(vote2);
        votingService.Vote(vote3);
        votingService.DisableVoting(workItem.Id, hostParticipant.Id);
        var savedWorkItem = votingService.GetWorkItem(workItem.Id);

        // assert
        savedWorkItem.Should().NotBeNull();
        var list = savedWorkItem.Votes.ToList();
        list.Count().Should().Be(3);
        list.First().Value.Should().BeSameAs(vote3);
        list.First().Key.Should().BeSameAs(ParticipantZ.Id);
        list[1].Value.Should().BeSameAs(vote1);
        list[1].Key.Should().BeSameAs(hostParticipant.Id);
        list[2].Value.Should().BeSameAs(vote2);
        list[2].Key.Should().BeSameAs(ParticipantA.Id);
    }
}
