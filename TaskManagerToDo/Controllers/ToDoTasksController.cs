using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerToDo.Data;
using TaskManagerToDo.Identity;
using TaskManagerToDo.Models;
using TaskManagerToDo.Service.Interface;

namespace TaskManagerToDo.Controllers
{
    public class ToDoTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IToDoTaskService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public ToDoTasksController(IToDoTaskService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string searchTerm = "", bool? isComplete = null)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return View();
            }

            return View(await _service.GetTasks(currentUser, searchTerm, isComplete));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _service.GetTaskById((int)id);
            if (toDoTask == null)
            {
                return NotFound();
            }

            return View(toDoTask);
        }

        public async Task<IActionResult> Create()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var toDoTask = new ToDoTask()
            {
                UserID = currentUser.Id
            };

            return View(toDoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateTask(toDoTask);

                return RedirectToAction(nameof(Index));
            }
            return View(toDoTask);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var toDoTask = await _service.GetTaskById((int)id);
            if (toDoTask == null)
            {
                return NotFound();
            }
            return View(toDoTask);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ToDoTask toDoTask)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    toDoTask = await _service.UpdateTask(toDoTask);
                }
                catch (Exception e)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(toDoTask);
        }

        public async Task<IActionResult> Delete(int id)
        {
            return View(await _service.GetTaskById(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var toDoTask = await _service.DeleteTask((int) id);

            return RedirectToAction(nameof(Index));
        }
    }
}
