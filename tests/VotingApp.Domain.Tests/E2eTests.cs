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
        var hostParticipant = new Participant("hostId", "host");        
        
        // act
        var workItem = votingService.CreateWorkItem(hostParticipant, "workitem", false);
        var vote1 = new Vote("voteId1", hostParticipant.Id, workItem.Id, 1);
        

        votingService.Vote(vote1);
        votingService.DisableVoting(workItem.Id, hostParticipant.Id);
        var savedWorkItem = votingService.GetWorkItem(workItem.Id);

        // assert
        savedWorkItem.Should().NotBeNull();
        savedWorkItem.Votes.First().Value.Should().BeSameAs(vote1);
        savedWorkItem.Votes.First().Key.Should().BeSameAs(hostParticipant.Id);
    }
}
