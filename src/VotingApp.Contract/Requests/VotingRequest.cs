using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Requests
{
    public class VotingRequest
    {
        public string Participant { get; set; }
        public int Value { get; set; }
    }
}
