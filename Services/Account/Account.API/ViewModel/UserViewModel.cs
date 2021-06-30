using Account.Domain.AggregatesModel.UserAggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.ViewModel
{
    public class UserViewModel
    {
        public static UserViewModel FromUser(User user)
        {
            return new UserViewModel()
            {
                IdentityUser = user.IdentityUser,
                Name = user.Name,
                Email = user.Email,
                CreatedDate = user.CreatedDate
            };
        }
        public string IdentityUser { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get;  set; }
    }

    public class UserUpdateViewModel
    {
        public static UserUpdateViewModel FromUser(User user)
        {
            return new UserUpdateViewModel()
            {
                Name = user.Name,
                Email = user.Email
            };
        }
        public string Name { get; set; }
        public string Email { get; set; }
    }

}
