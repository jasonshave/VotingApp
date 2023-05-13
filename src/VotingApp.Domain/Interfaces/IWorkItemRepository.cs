using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Interfaces
{
    public interface IWorkItemRepository : ICrudRepository<WorkItem>
    {
        IEnumerable<WorkItem> GetAllWorkItemsByName(string name);
    }

    public interface IParticipantRepository : IDoAllRepository<Participant>
    {

    }

    public interface IDoAllRepository<TData>
        where TData : IAggregateRoot
    {
        TData Save(TData data);

        TData Get(string id);
    }
}
