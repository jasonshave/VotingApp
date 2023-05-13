using VotingApp.Domain.Abstractions;

namespace VotingApp.Domain.Models
{
    public record Participant(string Id, string Name) : IAggregateRoot;
}
