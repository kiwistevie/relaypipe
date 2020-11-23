using System;

namespace Relaypipe.Domain
{
    public class Entry
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int ChangedBy { get; set; }
        public int ChannelId { get; set; }
        public int? SessionId { get; set; }
        public string Content { get; set; }
        public int? ResponseTo { get; set; }
        public int Type { get; set; } = 1
    }
}
