using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages.Tasks
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly TaskService _taskService;
        private readonly UserManager<User> _userManager;

        public CreateModel(TaskService taskService, UserManager<User> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        [BindProperty]
        public TaskItem TaskItem { get; set; } = new();

        [BindProperty]
        public List<Category> Categories { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
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

            TaskItem.UserId = _userManager.GetUserId(User);
            var success = await _taskService.AddTaskAsync(TaskItem);

            if (success)
            {
                TempData["Message"] = "Tarefa criada com sucesso!";
                TempData["MessageType"] = "success";
                return RedirectToPage("/Tasks/Index");
            }

            TempData["Message"] = "Erro ao salvar a tarefa.";
            TempData["MessageType"] = "danger";
            return Page();
        }
    }
}
