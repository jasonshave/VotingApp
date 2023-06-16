using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingApp.Contract.Requests;
public class CreateWorkItemRequest
{
    public ParticipantDto Host { get; set; }
    public string Names { get; set; }
    public bool IsAnonymous { get; set; }
}
