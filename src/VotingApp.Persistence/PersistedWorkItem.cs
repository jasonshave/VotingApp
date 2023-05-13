namespace VotingApp.Persistence
{
    public class PersistedWorkItem
    {
        public string Id { get; init; }
        public IDictionary<string, PersistedVote> Votes { get; } = new Dictionary<string, PersistedVote>();
        public PersistedParticipant Host { get; set; }
        public List<PersistedParticipant> Participants { get; } = new();

    }
}
