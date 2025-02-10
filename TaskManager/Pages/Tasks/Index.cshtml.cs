using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages.Tasks
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly TaskService _taskService;
        private readonly UserManager<User> _userManager;

        public IndexModel(TaskService taskService, UserManager<User> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        public List<TaskItem> Tasks { get; set; } = new();

        public async Task OnGetAsync()
        {
            var userId = _userManager.GetUserId(User);
            Tasks = await _taskService.GetTasksByUserAsync(userId);
        }

        public async Task<IActionResult> OnPostDeleteAsync(int taskId)
        {
            var userId = _userManager.GetUserId(User);
            var success = await _taskService.DeleteTaskAsync(taskId, userId);

            if (success)
            {
                TempData["Message"] = "Tarefa excluída com sucesso!";
                TempData["MessageType"] = "warning";
                return RedirectToPage("/Tasks/Index");
            }

            TempData["Message"] = "Erro ao excluir a tarefa.";
            TempData["MessageType"] = "danger";
            return Page();
        }
    }
}
