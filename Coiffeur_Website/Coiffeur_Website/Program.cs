using Coiffeur_Website.Data;
using Microsoft.EntityFrameworkCore;

namespace Coiffeur_Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<CoiffeurDbContext>(option => option.UseSqlServer
            (builder.Configuration.GetConnectionString("DefaultConnection")));
            

            builder.Services.AddSession(
            opt =>
            {
                opt.IdleTimeout = TimeSpan.FromMinutes(5);
            }
            );

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddDbContext<Data.CoiffeurDbContext>();


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
            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}