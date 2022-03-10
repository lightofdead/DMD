using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMD.Models
{
    public class Task
    {
        public Task()
        {
            Subtasks = new List<Task>();
        }

        public int ID { get; set; }
        
        public string? Name { get; set; }
        public int? ParentID { get; set; }
        public Task? ParentTask { get; set; }
        public ICollection<Task> Subtasks { get; set; }
    }
}
