using VotingApp.Domain.Abstractions;

namespace VotingApp.Domain.Models
{
    public class WorkItem : IAggregateRoot
    {
        public string Id { get; init; }

        public IDictionary<string, Vote> Votes { get; } = new Dictionary<string, Vote>();

        public Participant Host { get; set; }

        public List<Participant> Participants { get; } = new();
    }
}
