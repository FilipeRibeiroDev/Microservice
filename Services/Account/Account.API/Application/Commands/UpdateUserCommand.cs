using Account.API.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Account.API.Application.Commands
{
    [DataContract]
    public class UpdateUserCommand : IRequest<UserUpdateViewModel>
    {
        [DataMember]
        public string IdentityUser { get; private set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }

        public UpdateUserCommand()
        {

        }

        public UpdateUserCommand(string identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
