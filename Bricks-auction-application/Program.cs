using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;

var builder = WebApplication.CreateBuilder(args);

// Pobieranie danych konfiguracyjnych z pliku
var connectionString = builder.Configuration.GetConnectionString("System");

// Dodawanie do zasobów klasy kontekstu dla bazy danych
builder.Services.AddDbContext<BricksAuctionDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
