using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Pages.Account;

public class RegisterModel : PageModel
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [BindProperty]
    public RegisterDto Input { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = new User
        {
            UserName = Input.Email,
            Email = Input.Email,
            FullName = Input.FullName,
            BirthDate = Input.BirthDate,
            PhoneNumber = Input.PhoneNumber,
            RegistrationDate = DateTime.UtcNow.AddHours(-3)
        };

        var result = await _userManager.CreateAsync(user, Input.Password);

        if (result.Succeeded)
        {
            // Redireciona para o login após registro com sucesso
            return RedirectToPage("/Account/Login");
        }

        // Adiciona os erros retornados pelo Identity ao ModelState para exibição
        foreach (var error in result.Errors)
            // Adiciona cada erro ao ModelState
            ModelState.AddModelError(string.Empty, error.Description); 

        return Page();
    }

}
