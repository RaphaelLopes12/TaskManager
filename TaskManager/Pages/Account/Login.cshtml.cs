using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskManager.DTOs;
using TaskManager.Models;

namespace TaskManager.Pages.Account;

public class LoginModel : PageModel
{
    private readonly SignInManager<User> _signInManager;

    public LoginModel(SignInManager<User> signInManager)
    {
        _signInManager = signInManager;
    }

    [BindProperty]
    public LoginDto Input { get; set; } = new();

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, false);

        // Redireciona para as tarefas após login com sucesso
        if (result.Succeeded)
            return RedirectToPage("/Tasks/Index");

        ModelState.AddModelError(string.Empty, "Email ou senha inválidos. Verifique e tente novamente.");
        return Page();
    }
}
