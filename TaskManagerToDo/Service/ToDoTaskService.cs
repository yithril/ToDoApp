using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagerToDo.Data;
using TaskManagerToDo.Identity;
using TaskManagerToDo.Models;
using TaskManagerToDo.Service.Interface;

namespace TaskManagerToDo.Service
{
    public class ToDoTaskService : IToDoTaskService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ToDoTaskService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<ToDoTask>> GetTasks(ApplicationUser user, string searchTerm = "")
        {
            var toDos = _applicationDbContext.ToDoTasks.Where(x => x.UserID == user.Id);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                toDos = toDos.Where(x => x.Description.ToLower().Contains(searchTerm.ToLower()));
            }

            return await toDos.ToListAsync();
        }

        public async Task<ToDoTask> GetTaskById(int id)
        {
            return await _applicationDbContext.ToDoTasks.FindAsync(id);
        }

        public async Task<ToDoTask> UpdateTask(ToDoTask toDoTask)
        {

            _applicationDbContext.Update(toDoTask);

            await _applicationDbContext.SaveChangesAsync();

            return toDoTask;
        }

        public async Task<ToDoTask> CreateTask(ToDoTask toDoTask)
        {
            var result = await _applicationDbContext.ToDoTasks.AddAsync(toDoTask);

            await _applicationDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var toDelete = await _applicationDbContext.ToDoTasks.FindAsync(id);

            _applicationDbContext.Remove(toDelete);

            await _applicationDbContext.SaveChangesAsync();

            return true;
        }
    }
}
