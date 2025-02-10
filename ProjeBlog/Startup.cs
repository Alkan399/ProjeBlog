using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjeBlog.Context;
using ProjeBlog.Models;
using ProjeBlog.Models.ConstantModels;
using ProjeBlog.Models.FluentValidators;
using ProjeBlog.RepositoryPattern;
using ProjeBlog.RepositoryPattern.Base;
using ProjeBlog.RepositoryPattern.Concrete;
using ProjeBlog.RepositoryPattern.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using LoggerPrototype;
using Microsoft.Extensions.Logging;
using LoggerPrototype.Interfaces;
using LoggerPrototype.Interfaces.Base;
using LoggerPrototype.Interfaces.Concrete;
using ProjeBlog.RepositoryPattern.Services.Api;
using ProjeBlog.Middlewares;
using ProjeBlog.Models.LogModels;
using ProjeBlog.Services;

namespace ProjeBlog
{
    public class Startup
    {
        readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MyDbContext>(options => options.UseSqlServer(_configuration["ConnectionStrings:Mssql"]));
            services.AddControllersWithViews();
            services.AddFluentValidationAutoValidation(x => { x.DisableDataAnnotationsValidation = true; }).AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<AppUserValidator>();
            services.AddSingleton<ICustomLogger, CustomConsoleLogger>();
            services.AddSingleton<ICustomDbLogger>(provider => new CustomDbLogger(_configuration["ConnectionStrings:Mssql"]));
            services.AddScoped<IRepository<Basvuru>, Repository<Basvuru>>();
            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<IRepository<AppUser>, Repository<AppUser>>();
            services.AddScoped<IRepository<AppUserDetail>, Repository<AppUserDetail>>();
            services.AddScoped<IRepository<Content>, Repository<Content>>();
            services.AddScoped<IRepository<ContentSet>, Repository<ContentSet>>();
            services.AddScoped<IRepository<ContentSetContent>, Repository<ContentSetContent>>();
            services.AddScoped<IRepository<ContentSetElement>, Repository<ContentSetElement>>();
            services.AddScoped<IRepository<Comment>, Repository<Comment>>();
            services.AddScoped<IRepository<About>, Repository<About>>();
            services.AddScoped<IRepository<Statistics>, Repository<Statistics>>();
            services.AddScoped<IRepository<ContentStatistics>, Repository<ContentStatistics>>();
            services.AddScoped<IRepository<VisitorsLog>, Repository<VisitorsLog>>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IContentRepository, ContentRepository>();
            services.AddScoped<IContentSetContentRepository, ContentSetContentRepository>();
            services.AddScoped<IContentSetElementRepository, ContentSetElementRepository>();
            services.AddScoped<IStatisticsService<Content>, StatisticsService<Content>>();
            services.AddScoped<UtilityMethods>();
            services.AddScoped<IUtilityMethods, UtilityMethods>();
            services.AddHttpClient<IpApiService>();
            


            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
            {
                options.LoginPath = "/Blog/UserAuth/Login";
                options.Cookie.Name = "UserLoginDetail";
                options.Cookie.HttpOnly = false;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim("role", "admin"));
            });

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "DefaultArea",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}",
                defaults: new { area = "Blog", controller = "Home", action = "Index" }
                );
                endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllers();

            });
            app.UseMiddleware<VisitorMiddleware>();

        }
    }
}
