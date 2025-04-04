using Company.Zeinab4.BLL;
using Company.Zeinab4.BLL.Interfaces;
using Company.Zeinab4.BLL.Repostiors;
using Company.Zeinab4.DAL.Data.Context;
using Company.Zeinab4.DAL.Modules;
using Company.Zeinab4.PL.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Company.Zeinab4.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
        
            builder.Services.AddScoped<DepartmentRepostiory>();
        
            //builder.Services.TryAddScoped<EmployeeRepostiory>();


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<CompanyDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

        });
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new DepartmentProfile()));
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<CompanyDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(Config=>
            {
                Config.LoginPath = "/Account/SingIn";

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
            app.UseAuthentication();
            app.UseAuthorization();

        

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
