using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;

namespace VotingApp.Persistence;

public class AnotherWorkItemRepository : IWorkItemRepository
{
    private readonly ICrudRepository<WorkItem> _repo;

    public AnotherWorkItemRepository(ICrudRepository<WorkItem> repo)
    {
        _repo = repo;
    }

    public WorkItem Save(WorkItem model) => _repo.Save(model);

    public WorkItem Get(string id) => _repo.Get(id);

    public IEnumerable<WorkItem> GetAllWorkItemsByName(string name) => throw new NotImplementedException();
}