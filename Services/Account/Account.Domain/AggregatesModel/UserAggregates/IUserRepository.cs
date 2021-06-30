using Account.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.AggregatesModel.UserAggregates
{
    public interface IUserRepository : IRepository<User>
    {
        User Add(User user);
        User Update(string identity, User user);
        bool Remove(string identity);
    }
}
