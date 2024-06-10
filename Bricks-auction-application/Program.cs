using Microsoft.EntityFrameworkCore;
using Bricks_auction_application;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models.System.Respository;
using Bricks_auction_application.Models.System.Repository;
using Microsoft.AspNetCore.Identity;
using Bricks_auction_application.Models.Users;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Pobieranie danych konfiguracyjnych z pliku
var connectionString = builder.Configuration.GetConnectionString("System");
builder.Services.AddTransient<IEmailSender, EmailSender>();

// Dodawanie do zasobów klasy kontekstu dla bazy danych
builder.Services.AddDbContext<BricksAuctionDbContext>(x => x.UseSqlServer(connectionString));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<BricksAuctionDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserManager<User>>();

// Rejestracja repozytoriów
builder.Services.AddScoped<IOrderHeaderRepository, OrderHeaderRepository>();
builder.Services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();

var app = builder.Build();

var supportedCultures = new[]
            {
                    new CultureInfo("pl-PL")
            };

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pl-PL"),
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

app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

app.Run();
