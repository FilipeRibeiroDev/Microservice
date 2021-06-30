using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Account.API.Application.Commands
{
    [DataContract]
    public class RemoveUserCommand : IRequest<bool>
    {
        [DataMember]
        public string IdentityUser { get; private set; }
        public RemoveUserCommand()
        {

        }

        public RemoveUserCommand(string identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
