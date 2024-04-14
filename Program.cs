using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TestNetMVC.Models;
using TestNetMVC.Services;


namespace TestNetMVC;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);


    builder.Services.AddControllersWithViews();
    builder.Services.AddSession();
    var ConnectString = builder.Configuration["ConnectionStrings:DefaultConnection"];


    builder.Services.AddDbContext<TestnetmvcContext>(option =>
    {
      option.UseLazyLoadingProxies().UseSqlServer(ConnectString);
    });


    builder.Services.AddScoped<AccountService, AccountServiceImpl>();
    builder.Services.AddScoped<RoleService, RoleServiceImpl>();
    builder.Services.AddScoped<RequestService, RequestServiceImpl>();
    builder.Services.AddScoped<PriorityService, PriorityServiceImpl>();

    // cau hinh security
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
              option.LoginPath = "/home/login";
              // option.AccessDeniedPath = "/account/accessdenied";
            });


    var app = builder.Build();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseSession();
    app.UseRouting();

    app.UseAuthorization();
    app.MapControllerRoute(
      name: "myareas",
      pattern: "{area:exists}/{controller}/{action}");
    app.MapControllerRoute(
      name: "default",
      pattern: "{controller}/{action}");


    app.Run();
  }
}
