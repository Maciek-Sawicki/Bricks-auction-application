using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Microsoft.AspNetCore.Localization;
using System.Globalization;




var builder = WebApplication.CreateBuilder(args);

// Pobieranie danych konfiguracyjnych z pliku
var connectionString = builder.Configuration.GetConnectionString("System");

// Dodawanie do zasob�w klasy kontekstu dla bazy danych
builder.Services.AddDbContext<BricksAuctionDbContext>(x => x.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var supportedCultures = new[]
            {
                    new CultureInfo("en-US")
            };

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

app.UseRequestLocalization(localizationOptions);


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

