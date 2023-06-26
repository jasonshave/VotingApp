using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Responses
{
    public class GetVotedResponse
    {
        public IEnumerable<ParticipantDto> Participants { get; set; }
        public int VoteCount { get; set; }
    }
}
