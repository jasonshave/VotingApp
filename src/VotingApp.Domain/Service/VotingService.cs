﻿using System.Linq;
using Microsoft.Extensions.Logging;
using VotingApp.Domain.Abstractions;
using VotingApp.Domain.Interfaces;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Service
{
    public class VotingService : IVotingService
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
            workItem.Participants.Add(host);
            var savedWorkItem = _repository.Save(workItem);
            return savedWorkItem;
        }
       
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

        
        public WorkItem? GetWorkItem(string id)
        {
            try
            {
                var workItem = _repository.Get(id);
                var result = new WorkItem
                {
                    Id = workItem.Id,
                    Host = workItem.Host,
                    IsAnonymous = workItem.IsAnonymous,
                    VotingEnabled = workItem.VotingEnabled,
                };
                
                result.Participants.AddRange(workItem.Participants);
                var votes = workItem.Votes.OrderBy((pair) => pair.Value.Value);
                foreach (var pair in votes)
                {
                    result.Votes.Add(pair);
                }
                return result;
            }
            catch (NotFoundException e)
            {
                return null;
            }
        }

        
        public void EnableVoting(string workItemId, string participantId)
        {
            var workItem = _repository.Get(workItemId);

            if (workItem.Host.Id == participantId)
            {
                workItem.VotingEnabled = true;
            } 
            else
            {
                throw new ForbiddenException($"Participant {participantId} is not the host of workItem {workItemId}");
            }

            _repository.Save(workItem);

        }

        
        public void DisableVoting(string workItemId, string participantId)
        {
            var workItem = _repository.Get(workItemId);

            if (workItem.Host.Id == participantId)
            {
                workItem.VotingEnabled = false;
            }
            else
            {
                throw new ForbiddenException($"Participant {participantId} is not the host of workItem {workItemId}");
            }

            _repository.Save(workItem);
        }

        public void ClearVoting(string workItemId, string participantId)
        {
            var workItem = _repository.Get(workItemId);

            if (workItem.Host.Id == participantId)
            {
                workItem.Votes.Clear();
            }
            else
            {
                throw new ForbiddenException($"Participant {participantId} is not the host of workItem {workItemId}");
            }

            _repository.Save(workItem);
        }


        public void Vote(Vote vote)
        {
            var workItem = _repository.Get(vote.WorkItemId);

            if (!workItem.VotingEnabled)
            {
                throw new ForbiddenException($"Voting is disabled for ${vote.WorkItemId}");
            }

            //if (!workItem.Participants.Any(x => x.Id == vote.Participant.Id))
            //{
            //    throw new NotFoundException($"Participant ${vote.Participant.Id} is not found");
            //}

            workItem.Votes[vote.Participant.Id] = vote;
            _repository.Save(workItem);
        }
        
        public void AssignHost(string workItemId, string pariticipantId, string newHostId)
        {
            var workItem = _repository.Get(workItemId);
            if (workItem.Host.Id == pariticipantId)
            {
                var newHost = workItem.Participants.Find(x => x.Id == newHostId);
                if (newHost is not null)
                {
                    workItem.Host = newHost;
                    _repository.Save(workItem);
                }
                else
                {
                    throw new NotFoundException($"Participant{newHostId} is not found in workItem {workItemId}");
                }
            }
            else 
            {
                throw new ForbiddenException($"Participant {pariticipantId} is not host of workItem {workItemId}");
             }

        }


        public void SetAnonymous(string workItemId, string hostId, bool isAnonymous)
        {
            var workItem = _repository.Get(workItemId);
            if (workItem.Host.Id == hostId)
            {

                _repository.Save(workItem);
            }
        }
    }
}
