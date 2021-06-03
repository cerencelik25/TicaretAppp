
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicaretApp.Business.Abstract;
using TicaretApp.Business.Concrete;
using TicaretApp.DataAccess.Abstract;
using TicaretApp.DataAccess.Concrete.EfCore;
using TicaretDB.EmailServices;
using TicaretDB.Identity;
using TicaretDB.Middlewares;
using IEmailSender = TicaretDB.EmailServices.IEmailSender;

namespace TicaretDB
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options =>
             options.UseSqlServer(_configuration.GetConnectionString("IdentityConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;

                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                options.Lockout.AllowedForNewUsers = true;

                //options.User.AllowedUserNameCharacters = "";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedEmail = true;//onaylamasý için doðrulama maili
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logout";
                options.AccessDeniedPath = "/account/accessdenied";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                        Name=".TicaretApp.Security.Cookie"
                };
            });

            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<IKitapDal, EfCoreKitapDal>();
            services.AddScoped<IKategoriDal, EfCoreKategoriDal>();
            services.AddScoped<IKartDal, EfCoreKartDal>();
            services.AddScoped<ISiparisDal, EfCoreSipraisDal>();

            services.AddScoped<IKitapService, KitapManager>();
            services.AddScoped<IKategoriService, KategoriManager>();
            services.AddScoped<IKartService, KartManager>();
            services.AddScoped<ISiparisService, SiparisManager>();
            services.AddScoped<IEmailSender, SmtpEmailSender>(i=>
                new SmtpEmailSender(
                    _configuration["EmailSender:Host"],
                    _configuration.GetValue<int>("EmailSender:Port"),
                    _configuration.GetValue<bool>("EmailSender:EnableSSL"),
                    _configuration["EmailSender:UserName"],
                    _configuration["EmailSender:Password"]

                ));
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDatabase.Seed();
            }
            app.UseStaticFiles();
            app.CustomStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "adminKitaps",
                    pattern: "admin/kitaps/",
                    defaults: new { controller = "Admin", action = "KitapList" });

                endpoints.MapControllerRoute(
                   name: "kart",
                   pattern: "kart",
                   defaults: new { controller = "Kart", action = "Index" });

                endpoints.MapControllerRoute(
                  name: "checkout",
                  pattern: "checkout",
                  defaults: new { controller = "Kart", action = "Checkout" });

                endpoints.MapControllerRoute(
                    name: "adminKitaps",
                    pattern: "admin/kitaps/{id?}/",
                    defaults: new { controller = "Admin", action = "EditKitap" });

                endpoints.MapControllerRoute(
                    name: "kitaps",
                    pattern: "kitaps/{kategori?}/",
                    defaults: new { controller = "Kitap", action = "List" });

                endpoints.MapControllerRoute(
                   name: "siparis",
                   pattern: "siparis",
                   defaults: new { controller = "Kart", action = "GetSiparis" });

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            SeedIdentity.Seed(userManager, roleManager, _configuration).Wait();

        }
    }
}
