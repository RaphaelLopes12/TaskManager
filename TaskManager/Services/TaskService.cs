using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskItem>> GetTasksByUserAsync(string userId)
    {
        return await _context.Tasks
            .Where(t => t.UserId == userId)
            .Include(t => t.Category)
            .ToListAsync();
    }

    public async Task<TaskItem> GetTaskByIdAsync(int taskId, string userId)
    {
        return await _context.Tasks
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == taskId && t.UserId == userId);
    }

    public async Task<PagedResult<TaskItem>> GetPagedTasksAsync(string userId, int pageNumber, int pageSize, string searchTitle = "")
    {
        var query = _context.Tasks
                            .Where(t => t.UserId == userId);

        if (!string.IsNullOrWhiteSpace(searchTitle))
        {
            query = query.Where(t => t.Title.Contains(searchTitle));
        }

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        var tasks = await query
            .OrderByDescending(t => t.DueDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(t => t.Category)
            .ToListAsync();

        return new PagedResult<TaskItem>
        {
            Items = tasks,
            CurrentPage = pageNumber,
            TotalPages = totalPages,
            PageSize = pageSize
        };
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<bool> AddTaskAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateTaskAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteTaskAsync(int taskId, string userId)
    {
        var task = await GetTaskByIdAsync(taskId, userId);
        if (task == null) return false;

        _context.Tasks.Remove(task);
        return await _context.SaveChangesAsync() > 0;
    }
}
