using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Responses
{
    public class CreateWorkItemResponse
    {
        public string Id { get; init; }

        public ParticipantDto Host { get; set; }

        public string Name { get; set; }

        public bool VotingEnabled { get; set; }

        public bool IsAnonymous { get; set; }
    }
}
