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
    public class CreateUserCommand : IRequest<UserViewModel>
    {
        [DataMember]
        public string Name { get;  set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
    }
}
