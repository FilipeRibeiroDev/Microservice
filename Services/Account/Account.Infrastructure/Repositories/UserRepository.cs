using Account.Domain.AggregatesModel.UserAggregates;
using Account.Domain.Exceptions;
using Account.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Account.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AccountContext _context;
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        public UserRepository(AccountContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public User Add(User user)
        {
            var countUser = _context.User.Where(b => b.Email == user.Email).ToList();
            
            if(countUser.Count == 0)
            {
                _context.User.Add(user);
                _context.SaveChanges();
            }
            return _context.User.Where(b => b.Email == user.Email).First();
        }


        public bool Remove(string identity)
        {
            try
            {
                var userCurrent = _context.User.Where(b => b.IdentityUser == identity).First();
                if (userCurrent == null)
                {
                    throw new DomainException("Identity User not found!");
                }
                _context.User.Remove(userCurrent);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User Update(string identity, User user)
        {
            var userCurrent = _context.User.Where(b => b.IdentityUser == identity).First();
            if(userCurrent == null)
            {
                throw new DomainException("Identity User not found!");
            }
            userCurrent = user;
            _context.SaveChanges();

            return userCurrent;
        }
    }
}
