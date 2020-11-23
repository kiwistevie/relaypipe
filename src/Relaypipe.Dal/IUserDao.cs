using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Dal
{
    public interface IUserDao
    {
        IEnumerable<User> GetByUserName(string userName);
    }
}
