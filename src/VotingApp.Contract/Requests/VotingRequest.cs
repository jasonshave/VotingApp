﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Requests
{
    public class VotingRequest
    {
        public ParticipantDto Participant { get; set; }
        public int Value { get; set; }
    }
}
