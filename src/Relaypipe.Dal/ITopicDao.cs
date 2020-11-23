using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Dal
{
    public interface ITopicDao
    {
        IEnumerable<Topic> GetTopics(User user);
        void InsertTopic(Topic topic);
        void UpdateTopic(Topic topic);
        string GetUserPermission(User user, Topic topic);
        string GetGroupPermission(Group group, Topic topic);
        void SetUserPermission(User user, Topic topic, string permission);
        void SetGroupPermission(Group user, Topic topic, string permission);
    }
}
