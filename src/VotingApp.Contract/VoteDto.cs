using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract
{
    public class VoteDto
    {
        public string Id { get; set; }
        public ParticipantDto Participant { get; set; }
        public string WorkItemId { get; set; }
        public int Value { get; set; }
    }
}
