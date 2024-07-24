using FinPlus.Domain.CalendarOfDrops;
using FinPlus.Domain.Offers;
using FinPlus.Domain.Organisations;
using FinPlus.Domain.Sources;
using FinPlus.Domain.Users.Admin;
using FinPlus.Domain.Users.Drop;
using FinPlus.Domain.Users.Trafer;
using FinPlus.Infrastructure;
using FinPlus.Infrastructure.Models;
using FinPlusService;
using FinPlusService.Calendar;
using FinPlusService.DefualtBet;
using FinPlusService.Organisation;
using FinPlusService.Sources;
using FinPlusService.User.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettingsModel>(builder.Configuration.GetSection("MongoDB"));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IDropRepository, DropRepository>();
builder.Services.AddSingleton<IOfferRepository, OfferRepository>();
builder.Services.AddSingleton<IOfferService, OfferService>();
builder.Services.AddSingleton<ITrafferRepository, TrafferRepository>();
builder.Services.AddSingleton<IOrganisationsRepository, OrganisationRepository>();
builder.Services.AddSingleton<IDefaultBetRepository, DefaultBetRepository>();
builder.Services.AddSingleton<ICalendarRepository, CalendarRepository>();
builder.Services.AddSingleton<ICalendarService, CalendarService>();
builder.Services.AddSingleton<IAdminRepository, AdminRepository>();
builder.Services.AddSingleton<IAdminService, AdminService>();
builder.Services.AddSingleton<IDropService, DropService>();
builder.Services.AddSingleton<ITraferService, TraferService>();
builder.Services.AddSingleton<IDefaultBetService, DefaultBetService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ISourceRepository, SourceRepository>();
builder.Services.AddSingleton<ISourceService, SourceService>();
builder.Services.AddSingleton<IOrganisationService, OrganisationService>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(6000);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

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
