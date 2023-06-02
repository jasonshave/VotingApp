using Microsoft.Extensions.Logging;
using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Service
{
    public class VotingService
    {
        private readonly IWorkItemRepository _repository;
        private readonly ILogger<VotingService> _logger;

        public VotingService(IWorkItemRepository repository, ILogger<VotingService> logger)
        {
            _repository = repository;
            _logger = logger;
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
        public bool Join(string workItemId, Participant participant)
        {
            var workItem = _repository.Get(workItemId);
            var found = workItem.Participants.Any(p => p.Id == participant.Id);
            if (found)
            {
                workItem.Participants.Add(participant);
               _repository.Save(workItem);
            }
            return found;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        public bool Leave(string workItemId, string participantId)
        {
            var workItem = _repository.Get(workItemId);
            var targetParticipant = workItem.Participants.Find(p => p.Id == participantId);
            var found = targetParticipant != null;

            if (found)
            {
                workItem.Participants.Remove(targetParticipant);
                _repository.Save(workItem);
            }
            return found;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkItem? GetWorkItem(string id)
        {
            //ToDo: hide votes when voting is enabled
            try
            {
                var result = _repository.Get(id);
                return result;
            }
            catch (NotFoundException e)
            {
                return null;
            }
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
