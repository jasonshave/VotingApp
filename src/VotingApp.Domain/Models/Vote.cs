namespace VotingApp.Domain.Models
{
    public record Vote(string Id, string ParticipantId, string WorkItemId, int Value);
}
