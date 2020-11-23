using Dal.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Relaypipe.Dal.Dapper
{
    public class Repository<T> : IRepository<T> 
    {
        protected readonly IConnectionFactory connection;

        public Repository(IConnectionFactory connection)
        {
            this.connection = connection;
        }

        public IEnumerable<T> Get(string query, object arguments)
        {
            IList<T> entities;

            using (var connection = this.connection.CreateConnection())
            {
                connection.Open();
                entities = connection.Query<T>(query, arguments, commandType: CommandType.StoredProcedure).ToList();
            }

            return entities;
        }

        public T GetSingleOrDefault(string query, object arguments)
        {
            T entity;

            using (var connection = this.connection.CreateConnection())
            {
                connection.Open();
                entity =
                    connection.Query<T>(query, arguments, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }

            return entity;
        }

        public void Update(string query, object arguments)
        {
            using (var connection = this.connection.CreateConnection())
            {
                connection.Open();
                connection.Execute(query, arguments, commandType: CommandType.StoredProcedure);
            }
        }

        public int ExecuteScalar(string query, object arguments)
        {
            var id = 0;
            using (var connection = this.connection.CreateConnection())
            {
                connection.Open();
                id = connection.ExecuteScalar<int>(query, arguments, commandType: CommandType.StoredProcedure);
            }
            return id;
        }
    }
}
