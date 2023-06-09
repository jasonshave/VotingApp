using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Domain.Models;

namespace VotingApp.Domain.Interfaces
{
    public interface IVotingService
    {
        /// <summary>
        ///  create work item
        /// </summary>
        /// <param name="host"></param>
        /// <param name="name"></param>
        /// <param name="isAnonymous"></param>
        /// <returns></returns>
        WorkItem CreateWorkItem(Participant host, string name, bool isAnonymous);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participant"></param>
        bool Join(string workItemId, Participant participant);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        bool Leave(string workItemId, string participantId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WorkItem? GetWorkItem(string id);

        /// <summary>
        /// check whether request is from host if so enable voting
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        /// <exception cref="ApplicationException"></exception>
        void EnableVoting(string workItemId, string participantId);
        /// <summary>
        /// check whether request is from host if so disable voting
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        public void DisableVoting(string workItemId, string participantId);

        /// <summary>
        /// Voting on a workitem
        /// </summary>
        /// <param name="participantId"></param>
        /// <param name="workItemId"></param>
        /// <param name="vote"></param>
        public void Vote(Vote vote);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="pariticipantId"></param>
        /// <param name="newHostId"></param>
        public void AssignHost(string workItemId, string pariticipantId, string newHostId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="isAnonymous"></param>
        public void SetAnonymous(string workItemId, bool isAnonymous);
    }
}
