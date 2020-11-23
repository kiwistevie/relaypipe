using Dal.Common;
using Dapper;
using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Dal.Dapper
{
    public class TopicDao : ITopicDao
    {
        private readonly IConnectionFactory factory;
        private Repository<Topic> repo;

        public TopicDao(IConnectionFactory factory)
        {
            repo = new Repository<Topic>(factory);
            this.factory = factory;
        }

        public string GetGroupPermission(Group group, Topic topic)
        {
            string sql =
                @"SELECT Permission FROM GroupTopic WHERE GroupId = @GroupId AND TopicId = @TopicId";
            using (var connection = this.factory.CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<string>(sql, new { GroupId = group.Id, TopicId = topic.Id });
            }
        }

        public IEnumerable<Topic> GetTopics(User user)
        {
            string sql = 
                @"SELECT Id, CreatedAt, CreatedBy, ChangedAt, ChangedBy, Title, GroupId, Active, DoneDate 
                  FROM Topic
                  WHERE CreatedBy = @Id";
            return repo.Get(sql, user);
        }

        public string GetUserPermission(User user, Topic topic)
        {
            string sql =
                @"SELECT Permission FROM UserTopic WHERE UserId = @UserId AND TopicId = @TopicId";
            using (var connection = this.factory.CreateConnection())
            {
                connection.Open();
                return connection.QueryFirstOrDefault<string>(sql, new { UserId = user.Id, TopicId = topic.Id });
            }
        }

        public void InsertTopic(Topic topic)
        {
            string sql =
                @"INSERT INTO Topics (ChangedAt, ChangedBy, Title, GroupId, Active, DoneDate) 
                  VALUES (@ChangedAt, @ChangedBy, @Title, @GroupId, @Active, @DoneDate); SELECT SCOPE_IDENTITY()";
            topic.Id = repo.ExecuteScalar(sql, topic);
        }

        public void SetGroupPermission(Group group, Topic topic, string permission)
        {
            string dbPermission = GetGroupPermission(group, topic);
            string sql;
            if (dbPermission != null && dbPermission == permission) return;
            if (dbPermission != null)
            {
                sql = "UPDATE GroupTopic SET Permission = @Permission WHERE GroupId = @GroupId AND TopicId = @TopicId";
            } else
            {
                sql = "INSERT INTO GroupTopic (GroupId, TopicId, Permission) VALUES (@GroupId, @TopicId, @Permission)";
            }
            
            using (var connection = this.factory.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql, new { GroupId = group.Id, TopicId = topic.Id, Permission = permission });
            }
        }

        public void SetUserPermission(User user, Topic topic, string permission)
        {
            string dbPermission = GetUserPermission(user, topic);
            string sql;
            if (dbPermission != null && dbPermission == permission) return;
            if (dbPermission != null)
            {
                sql = "UPDATE UserTopic SET Permission = @Permission WHERE UserId = @UserId AND TopicId = @TopicId";
            }
            else
            {
                sql = "INSERT INTO UserTopic (UserId, TopicId, Permission) VALUES (@UserId, @TopicId, @Permission)";
            }

            using (var connection = this.factory.CreateConnection())
            {
                connection.Open();
                connection.Execute(sql, new { UserId = user.Id, TopicId = topic.Id, Permission = permission });
            }
        }

        public void UpdateTopic(Topic topic)
        {
            string sql =
                @"UPDATE Topics SET ChangedAt = @ChangedAt, ChangedBy = @ChangedBy, Title = @Title, GroupId = @GroupId, Active = @Active, DoneDate = @DoneDate";
            repo.Update(sql, topic);
        }
    }
}
