using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages.Tasks
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly TaskService _taskService;
        private readonly UserManager<User> _userManager;

        public EditModel(TaskService taskService, UserManager<User> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();

        [BindProperty]
        public List<Category> Categories { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var userId = _userManager.GetUserId(User);
            TaskItem = await _taskService.GetTaskByIdAsync(id, userId);

            if (TaskItem == null)
                return NotFound();

            Categories = await _taskService.GetCategoriesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _taskService.GetCategoriesAsync();
                return Page();
            }

            var userId = _userManager.GetUserId(User);
            TaskItem.UserId = userId;

            var success = await _taskService.UpdateTaskAsync(TaskItem);

            if (success)
            {
                TempData["Message"] = "Tarefa atualizada com sucesso!";
                TempData["MessageType"] = "info";
                return RedirectToPage("/Tasks/Index");
            }

            TempData["Message"] = "Erro ao atualizar a tarefa.";
            TempData["MessageType"] = "danger";
            return Page();
        }
    }
}
