using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagerToDo.Identity;
using TaskManagerToDo.Models;

namespace TaskManagerToDo.Service.Interface
{
    public interface IToDoTaskService
    {
        Task<List<ToDoTask>> GetTasks(ApplicationUser user, string searchTerm = "");
        Task<ToDoTask> GetTaskById(int id);
        Task<ToDoTask> UpdateTask(ToDoTask toDoTask);
        Task<ToDoTask> CreateTask(ToDoTask toDoTask);
        Task<bool> DeleteTask(int id);
    }
}
