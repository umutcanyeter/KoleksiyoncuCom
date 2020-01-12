using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoleksiyoncuCom.Bussiness.Abstract;
using KoleksiyoncuCom.Bussiness.Concrete;
using KoleksiyoncuCom.DataAccess.Abstract;
using KoleksiyoncuCom.DataAccess.Concrete.EntityFramework;
using KoleksiyoncuCom.WebUi.EmailServices;
using KoleksiyoncuCom.WebUi.Identity;
using KoleksiyoncuCom.WebUi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoleksiyoncuCom.WebUi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationIdentityDbContext>(options => options.UseSqlServer(@"Server=UMUTCAN\SQLEXPRESS;Initial Catalog=KoleksiyoncuCom;Integrated Security=true"));
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ISellerService, SellerManager>();
            services.AddScoped<IBuyerService, BuyerManager>();
            services.AddScoped<IUsersAndSellersService, UsersAndSellersManager>();
            services.AddScoped<IUsersAndBuyersService, UsersAndBuyersManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<IOrderService, OrderManager>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ISellerDal, EfSellerDal>();
            services.AddScoped<IBuyerDal, EfBuyerDal>();
            services.AddScoped<IUsersAndSellersDal, EfUsersAndSellersDal>();
            services.AddScoped<IUsersAndBuyersDal, EfUsersAndBuyersDal>();
            services.AddScoped<ICartDal, EfCartDal>();
            services.AddScoped<IOrderDal, EfOrderDal>();
            services.AddTransient<IEmailSender,EmailSender>();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(7);
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/hesap/girisyap";
                options.LogoutPath = "/hesap/cikisyap";
                options.AccessDeniedPath = "/hesap/girisred";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".KoleksiyoncuCom.Security.Cookie"
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseAuthentication();
            app.CustomStaticFiles();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
