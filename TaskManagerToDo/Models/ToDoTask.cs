using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskManagerToDo.Identity;

namespace TaskManagerToDo.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("Due Date")]
        public DateTime DueDate { get; set; }

        [DisplayName("Complete?")]
        public bool IsComplete { get; set; }

        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        public string UserID { get; set; }
    }
}
