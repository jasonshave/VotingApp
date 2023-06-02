using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Interfaces
{
    public interface IWorkItemRepository : ICrudRepository<WorkItem>
    {
        IEnumerable<WorkItem> GetAllWorkItemsByName(string name);
    }
}
