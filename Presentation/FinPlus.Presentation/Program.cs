using FinPlus.Domain.CalendarOfDrops;
using FinPlus.Domain.Offers;
using FinPlus.Domain.Organisations;
using FinPlus.Domain.Users.Admin;
using FinPlus.Domain.Users.Drop;
using FinPlus.Domain.Users.Trafer;
using FinPlus.Infrastructure;
using FinPlus.Infrastructure.Models;
using FinPlusService;
using FinPlusService.DefualtBet;
using FinPlusService.User.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettingsModel>(builder.Configuration.GetSection("MongoDB"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(600);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddSingleton<IDropRepository, DropRepository>();
builder.Services.AddSingleton<IOfferRepository, OfferRepository>();
builder.Services.AddSingleton<ITrafferRepository, TrafferRepository>();
builder.Services.AddSingleton<IOrganisationsRepository, OrganisationRepository>();
builder.Services.AddSingleton<IDefaultBetRepository, DefaultBetRepository>();
builder.Services.AddSingleton<ICalendarRepository, CalendarRepository>();
builder.Services.AddSingleton<IAdminRepository, AdminRepository>();
builder.Services.AddSingleton<IAdminService, AdminService>();
builder.Services.AddSingleton<IDropService, DropService>();
builder.Services.AddSingleton<ITraferService, TraferService>();
builder.Services.AddSingleton<IDefaultBetService, DefaultBetService>();
builder.Services.AddSingleton<IAuthService, AuthService>();

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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
