using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Service
{
    public class VotingService
    {
        private readonly IWorkItemRepository _repository;

        public VotingService(IWorkItemRepository repository)
        {
            _repository = repository;
        }

        public WorkItem CreateWorkItem(Participant host)
        {
            var workItem = new WorkItem()
            {
                Id = Guid.NewGuid().ToString(),
                Host = host,
            };
            var savedWorkItem = _repository.Save(workItem);
            return savedWorkItem;
        }
    }
}
