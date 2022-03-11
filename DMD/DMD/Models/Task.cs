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
        
        //планировалось ввести остальные свойства модели в конце, так это не так трудоемко,
        //как понять ajax+Tree

        /*
        public (Class enum) Status { get; set; }
        public string Description { get; set; }
        public string Executors { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public int PlanTime { get; set; }
        public TimeSpan FactTime
        {
            get { return CompletionDate - RegisterDate; }
        }*/
    }
}
