using System;

namespace Relaypipe.Domain
{
    public class Topic
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int? ChangedBy { get; set; }
        public string Title { get; set; }
        public int GroupId { get; set; }
        public bool Active { get; set; }
        public DateTime? DoneDate { get; set; }
    }
}
