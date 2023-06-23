using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract
{
    public class VoteDto
    {
        public string ParticipantId { get; set; }
        public int Value { get; set; }
    }
}
