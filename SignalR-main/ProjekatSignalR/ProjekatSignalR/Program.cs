using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjekatSignalR.Data;
using ProjekatSignalR.Models;
using ProjekatSignalR.Hubs;

var builder = WebApplication.CreateBuilder(args); // ukljucujemo SignalR u projekat

// ==== Konfiguracija baze ====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// ==== Identity konfiguracija ====
builder.Services.AddDefaultIdentity<Korisnik>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// ==== Dodavanje MVC i RazorPages ====
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// ==== Dodavanje SignalR ====
builder.Services.AddSignalR();
builder.Services.AddAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // za css, js, slike itd.
app.UseRouting();

app.UseCors(); // za SignalR

app.UseAuthentication(); // obavezno zbog Identity
app.UseAuthorization();

// ==== Mapiranje kontrolera i Razor Pages ====

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

// ==== Mapiranje SignalR ====
app.MapHub<ChatHub>("/chathub"); // ruta na kojoj frontend povezuje SignalR

// ==== Pokretanje aplikacije ====
app.Run();
