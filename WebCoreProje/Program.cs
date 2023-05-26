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
            { opt.UseSqlServer(builder.Configuration.GetConnectionString("StandartBaglanti"));
                //opt.UseLazyLoadingProxies  BA�LANTILI TABLOLAR ��� ARA�TIR

            });//database i�in servisi ekledik, connection string leri olu�turduk. App setting e ta��d�k oradan okuyal�m

            var app = builder.Build();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt => 
            { 
                opt.Cookie.Name="UserAuthenticate";
                opt.ExpireTimeSpan = TimeSpan.FromDays(1);//ne zaman silinsin
                opt.SlidingExpiration = false; //�teleme yap�p s�re ekliyim mi
                opt.LoginPath = "/Acoount/Login";
             
            });


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

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}