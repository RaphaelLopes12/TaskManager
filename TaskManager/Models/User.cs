using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;

public class User : IdentityUser
{
    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(User), nameof(ValidateBirthDate))]
    public DateTime BirthDate { get; set; }

    // Horário de Brasília (GMT-3)
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow.AddHours(-3); 

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();

    // Validação para garantir que a data de nascimento não seja maior que hoje
    public static ValidationResult ValidateBirthDate(DateTime birthDate, ValidationContext context)
    {
        if (birthDate >= DateTime.UtcNow.AddHours(-3))
        {
            return new ValidationResult("A data de nascimento não pode ser no futuro.");
        }
        return ValidationResult.Success!;
    }
}
