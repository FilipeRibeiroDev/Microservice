using Account.Domain.Exceptions;
using Account.Domain.Seedwork;
using Account.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.AggregatesModel.UserAggregates
{
    public class User: Entity, IAggregateRoot
    {
        public string IdentityUser { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedDate { get; private set; }

        public User(string name, string email, string password)
        {
            this.IdentityUser = Guid.NewGuid().ToString();
            this.Name = !string.IsNullOrEmpty(name) ? name : throw new DomainException("Invalid Name");
            this.Email = EmailText.IsValid(email) ? email : throw new DomainException("Invalid Email");
            this.Password =  PasswordPolicy.IsValid(password) ? password : throw new DomainException("Invalid Password! The password must contain at least 1 uppercase character, numbers and at least 6 characters.");
            this.CreatedDate = DateTime.UtcNow;
        }

    }
}
