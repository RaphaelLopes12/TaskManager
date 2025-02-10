using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TaskManager.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TaskManager.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [DataType(DataType.Date)]
    [Range(typeof(DateTime), "1900-01-01", "2099-12-31", ErrorMessage = "A data deve estar entre 1900 e 2099.")]
    public DateTime DueDate { get; set; }

    public TaskState Status { get; set; } = TaskState.Pendente;

    // Relacionamento com o Usuário    
    [BindNever]
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public User? User { get; set; }

    // Relacionamento com a Categoria
    [Required(ErrorMessage = "A categoria é obrigatória.")]
    public int? CategoryId { get; set; }
    public Category? Category { get; set; }
}
