using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.Models;
using TaskManager.Services;

namespace TaskManager.Pages.Tasks;

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
    public int CurrentPage { get; set; } = 1;
    public int TotalPages { get; set; }
    public int PageSize { get; set; } = 5;

    [BindProperty(SupportsGet = true)]
    public string SearchTitle { get; set; } = string.Empty;

    public async Task<IActionResult> OnGetAsync(int pageNumber = 1, int pageSize = 5)
    {
        var userId = _userManager.GetUserId(User);
        var result = await _taskService.GetPagedTasksAsync(userId, pageNumber, pageSize, SearchTitle);

        Tasks = result.Items;
        CurrentPage = result.CurrentPage;
        TotalPages = result.TotalPages;
        PageSize = result.PageSize;

        return Page();
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
