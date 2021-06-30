using Account.API.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Account.API.Application.Queries
{
    public interface IUserQueries
    {
        Task<List<UserViewModel>> GetUsers();

        Task<UserViewModel> GetUser(string id);
    }
}
