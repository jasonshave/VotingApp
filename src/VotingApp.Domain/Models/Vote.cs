namespace VotingApp.Domain.Models
{
    public record Vote(string Id, Participant Participant, string WorkItemId, int Value);
}
