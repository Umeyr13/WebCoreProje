using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebCoreProje.Models;

namespace WebCoreProje
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<DatabaseContext>(opt => 
            { opt.UseSqlServer(builder.Configuration.GetConnectionString("StandartBaglanti"), sqlServerOption => sqlServerOption.EnableRetryOnFailure());
                //opt.UseLazyLoadingProxies  BAÐLANTILI TABLOLAR ÝÇÝ ARAÞTIR

            });//database için servisi ekledik, connection string leri oluþturduk. App setting e taþýdýk oradan okuyalým

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                opt.Cookie.Name = "UserAuthenticate";
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);//ne zaman silinsin
                opt.SlidingExpiration = false; //Öteleme yapýp süre ekliyim mi
                opt.LoginPath = "/Account/Login";//Giriþ yapmadýysa buraya at
                opt.LogoutPath = "/Account/Login";//Çýkýþ yaparsa buraya at
                opt.AccessDeniedPath = "/Home/AccessDenied";//Yetkisi yoksa buraya at
             
            });

            var app = builder.Build();





            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            //app.UseSession(); Kullanmak istersen buraya eklemek Gerekli

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();// Önce Giriþi kontrol et
            app.UseAuthorization();// Sonra rolleri kontrol et
           

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}