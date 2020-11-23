using System;

namespace Relaypipe.Domain
{
    public class Point
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ChangedAt { get; set; }
        public int ChangedBy { get; set; }
        public int TopicId { get; set; }
        public int ParentPointId { get; set; }
        public int CopyFromPointId { get; set; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public int StatusId { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? DoneDate { get; set; }
        public bool Active { get; set; }
    }
}
