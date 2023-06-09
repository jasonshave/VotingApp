﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingApp.Domain.Models;
using VotingApp.Domain.Abstractions;

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
        ///  A participant to join the workitem
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participant"></param>
        bool Join(string workItemId, Participant participant);

        /// <summary>
        /// A participant to leave the workitem
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        bool Leave(string workItemId, string participantId);

        /// <summary>
        /// getting the workitem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        WorkItem? GetWorkItem(string id);

        /// <summary>
        /// check whether request is from host if so enable voting
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        /// <exception cref="ApplicationException" />
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
        /// <param name="vote"></param>
        /// <exception cref="ApplicationException" />
        /// <exception cref="NotFoundException" />
        public void Vote(Vote vote);

        /// <summary>
        ///  Assign new host for a work item
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="pariticipantId"></param>
        /// <param name="newHostId"></param>
        /// <exception cref="ApplicationException" />
        /// <exception cref="NotFoundException" />
        public void AssignHost(string workItemId, string pariticipantId, string newHostId);

        /// <summary>
        /// Clear the workItem
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="participantId"></param>
        void ClearVoting(string workItemId, string participantId);

        /// <summary>
        ///  Set the Workitem to be Anonymous
        /// </summary>
        /// <param name="workItemId"></param>
        /// <param name="hostId"></param>
        /// <param name="isAnonymous"></param>
        public void SetAnonymous(string workItemId, string hostId, bool isAnonymous);
    }
}
