using Microsoft.Extensions.Logging;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;
using VotingApp.Infrastructure.InMemoryDatabase;

namespace VotingApp.Persistence;

public class WorkItemRepository : ConcurrentMemoryBase<WorkItem>, IWorkItemRepository
{
    public WorkItemRepository(ILogger<WorkItemRepository> logger)
        : base(logger)
    {

    }

    public IEnumerable<WorkItem> GetAllWorkItemsByName(string name) => throw new NotImplementedException();
}