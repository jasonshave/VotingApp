using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Responses
{
    public class GetWorkItemResponse
    {
        public string Id { get; init; }

        // Key is ParticipantId
        public IEnumerable<VoteDto>? Votes { get; set; }

        public ParticipantDto Host { get; set; }

        public string Name { get; set; }

        public bool VotingEnabled { get; set; }

        public bool IsAnonymous { get; set; }

        public IEnumerable<ParticipantDto> Participants { get; set; }
    }
}
