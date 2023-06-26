using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Requests
{
    public class VotingRequest
    {
        [Required]
        public string ParticipantId { get; set; }
        [Required]
        public int Value { get; set; }
    }
}
