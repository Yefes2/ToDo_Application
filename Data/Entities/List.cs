using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class List
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key - The User who owns the List
        public int UserId { get; set; }
        public virtual User User { get; set; }

        // Navigation Property - The Tasks that belong to this List
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
