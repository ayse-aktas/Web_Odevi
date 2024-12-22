using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Coiffeur_Website
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            ConfigureMiddleware(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // MVC ve Controller yapılandırması
            services.AddControllersWithViews();

            // Session ayarları
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Oturum süresi
                options.Cookie.HttpOnly = true; // XSS saldırılarına karşı güvenlik
                options.Cookie.IsEssential = true; // GDPR uyumluluğu
            });

            // HttpContext erişimi için
            services.AddHttpContextAccessor();

            // Veritabanı bağlantısı
            services.AddDbContext<Data.CoiffeurDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Authentication ayarları (CookieAuthentication)
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Musteri/MusteriLogin"; // Oturum açma yönlendirmesi
                    options.LogoutPath = "/Musteri/Logout"; // Oturum kapama yönlendirmesi
                    options.AccessDeniedPath = "/Home/AccessDenied"; // Yetki reddedilirse yönlendirme
                });
        }

        private static void ConfigureMiddleware(WebApplication app)
        {
            // Hata işleme
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // Hata sayfasına yönlendirme
                app.UseHsts(); // HSTS protokolünü etkinleştir (HTTPS güvenliği)
            }

            // HTTPS yönlendirmesi ve statik dosya kullanımı
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Routing yapılandırması
            app.UseRouting();

            // Authentication ve Authorization middleware'lerini etkinleştir
            app.UseAuthentication();
            app.UseAuthorization();

            // Session Middleware
            app.UseSession();

            // Varsayılan rota
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
