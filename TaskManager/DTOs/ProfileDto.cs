using System.ComponentModel.DataAnnotations;
using TaskManager.Validators;

namespace TaskManager.DTOs;

public class ProfileDto
{
    [Required(ErrorMessage = "O nome completo é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome completo deve ter no máximo 100 caracteres.")]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
    [DataType(DataType.Date)]
    [BirthDateValidation]
    public DateTime BirthDate { get; set; }

    [Phone(ErrorMessage = "Número de telefone inválido.")]
    public string? PhoneNumber { get; set; }
}
