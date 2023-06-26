using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Responses
{
    public class CloseVotingResponse
    {
        public string Name { get; set; }
        public IEnumerable<VoteDto> Votes { get; set; }
        public int AverageVote { get; set; }
    }
}
