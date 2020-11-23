using Dal.Common;
using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Dal.Dapper
{
    public class UserDao : IUserDao
    {
        private IRepository<User> repo;

        public UserDao(IConnectionFactory connectionFactory)
        {
            repo = new Repository<User>(connectionFactory);
        }

        public IEnumerable<User> GetByUserName(string userName)
        {
            return repo.Get("SELECT Id, FirstName, LastName, UserName, Active FROM Users WHERE UserName = @userName", userName);
        }
    }
}
