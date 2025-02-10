using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;
using TaskManager.Services;

var builder = WebApplication.CreateBuilder(args);

// Configurar o banco de dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Adicionar autenticação com Identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<PortugueseIdentityErrorDescriber>();

// Registra os serviços no DI (Dependency Injection)
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TaskService>();

// Adicionar Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// O middleware de autenticação e autorização deve vir ANTES do UseEndpoints
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

app.MapGet("/", async (HttpContext context) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        // Redireciona para a listagem de tarefas se estiver logado
        context.Response.Redirect("/Tasks/Index");
    }
    else
    {
        // Se não estiver logado, manda para a tela de login
        context.Response.Redirect("/Account/Login"); 
    }
    await Task.CompletedTask;
});


app.Run();
