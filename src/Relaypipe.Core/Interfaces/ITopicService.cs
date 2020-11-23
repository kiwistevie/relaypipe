using Relaypipe.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Relaypipe.Core.Interfaces
{
    public interface ITopicService
    {
        IEnumerable<Topic> GetTopics(User user, string title, bool active);
        void SetTopicActive(User user, Topic topic, bool active);
        void SetTopicDone(User user, Topic topic);
        void CreateTopic(User user, Topic topic);
        void UpdateTopic(User user, Topic topic);
    }
}
