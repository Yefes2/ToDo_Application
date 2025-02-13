using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Status (Pending, Completed, Cancelled)
        public TaskStatus Status { get; set; }

        // Optional Due Date
        public DateTime? DueDate { get; set; }

        // Foreign Key - The List this Task belongs to
        public int ListId { get; set; }
        public virtual List List { get; set; }
    }

    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }
}
