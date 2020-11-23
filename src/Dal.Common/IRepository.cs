using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Common
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get(string query, object arguments);

        T GetSingleOrDefault(string query, object arguments);

        void Update(string query, object arguments);

        int ExecuteScalar(string query, object arguments);
    }
}
