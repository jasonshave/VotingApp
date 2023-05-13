namespace VotingApp.Persistence
{
    public record PersistedVote(string Id, PersistedParticipant Participant, string WorkItemId, int Value);
}
