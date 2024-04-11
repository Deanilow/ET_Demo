using ET.DataAccess.Repository;
using ET.Domain.Interface.ICore;
using ET.Domain.Interface.IRepository;
using ET.Domain.Interface.IService;
using ET.Domain.Services;
using ET.Web.Core;
using Microsoft.AspNetCore.Authentication.Cookies;

Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "Development");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddTransient<IAutenticacionService, AutenticacionService>();
builder.Services.AddTransient<ISedeOlimpicaService, SedeOlimpicoService>();
builder.Services.AddTransient<IComplejoDeportivoService, ComplejoDeportivoService>();
builder.Services.AddTransient<IAutenticacionRepository, AutenticacionRepository>();
builder.Services.AddTransient<ISedeOlimpicaRepository, SedeOlimpicaRepository>();
builder.Services.AddTransient<IComplejoDeportivoRepository, ComplejoDeportivoRepository>();
builder.Services.AddTransient<IPasswordService, PasswordService>();
builder.Services.AddMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "ETV1.Auth";
    options.IdleTimeout = TimeSpan.FromDays(Convert.ToInt32(1));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
      options.Cookie.Name = "ETV1.Auth";
      options.ExpireTimeSpan = TimeSpan.FromDays(Convert.ToInt32(1));
      options.LoginPath = new PathString("/Login");
      options.AccessDeniedPath = new PathString("/Login/LogOut");
      options.LogoutPath = new PathString("/Login/LogOut");
  });

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromDays(Convert.ToInt32(1));
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
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
app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
