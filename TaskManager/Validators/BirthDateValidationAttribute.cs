using System.ComponentModel.DataAnnotations;

namespace TaskManager.Validators;

public class BirthDateValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            if (birthDate >= DateTime.UtcNow.AddHours(-3))
            {
                return new ValidationResult("A data de nascimento não pode ser no futuro.");
            }
        }
        return ValidationResult.Success!;
    }
}
