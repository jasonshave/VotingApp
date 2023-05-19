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

        public WorkItem CreateWorkItem(Participant host, string name, bool isAnonymous)
        {
            var workItem = new WorkItem()
            {
                Id = Guid.NewGuid().ToString(),
                Host = host,
                Name = name,
                VotingEnabled = true,
                IsAnonymous = isAnonymous
            };
            var savedWorkItem = _repository.Save(workItem);
            return savedWorkItem;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participant"></param>
        public void Join(string workItemId, Participant participant)
        {
            var workItem = _repository.Get(workItemId);
            if (!workItem.Participants.Contains(participant))
            {
                workItem.Participants.Add(participant);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        public void Leave(string workItemId, string participantId)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkItem GetWorkItem(string id)
        {
            //ToDo: hide votes when voting is enabled
            return _repository.Get(id);
        }

        /// <summary>
        /// check wether request is from host if so enable voting
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        public void EnableVoting(string workItemId, string participantId)
        {

        }

        /// <summary>
        /// check wether request is from host if so disable voting
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        public void DisableVoting(string workItemId, string participantId)
        {

        }

        /// <summary>
        /// Voting on a workitem
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="workItemId"></param>
        /// <param name="vote"></param>
        public void Vote(string workItemId, string participantId, int vote)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="pariticipantId"></param>
        /// <param name="newHostId"></param>
        public void AssignHost(string workItemId, string pariticipantId, string newHostId)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="isAnonymous"></param>
        public void SetAnonymous(string  workItemId, bool isAnonymous)
        {

        }
    }
}
