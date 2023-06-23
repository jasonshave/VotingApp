using VotingApp.Domain.Abstractions;

namespace VotingApp.Domain.Models
{
    public record Participant: IAggregateRoot
    {
        public required string Id { get; init; }
        public string Name { get; set; }
    }
}
