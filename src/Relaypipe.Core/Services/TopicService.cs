using Relaypipe.Core.Interfaces;
using Relaypipe.Dal;
using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Core.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicDao topicDao;

        public TopicService(ITopicDao topicDao)
        {
            this.topicDao = topicDao;
        }

        public void CreateTopic(User user, Topic topic)
        {
            topicDao.InsertTopic(topic);
            topicDao.SetUserPermission(user, topic, Permission.READWRITEEXECUTE.ToPermissionString());
        }

        public IEnumerable<Topic> GetTopics(User user, string title, bool active)
        {
            throw new NotImplementedException();
        }

        public void SetTopicActive(User user, Topic topic, bool active)
        {
            throw new NotImplementedException();
        }

        public void SetTopicDone(User user, Topic topic)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(User user, Topic topic)
        {
            throw new NotImplementedException();
        }
    }
}
