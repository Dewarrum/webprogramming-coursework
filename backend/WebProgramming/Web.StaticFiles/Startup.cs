using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Common;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Services;
using Web.Common.App;
using Web.StaticFiles.App;
using FileSystemImageService = Web.StaticFiles.App.FileSystemImageService;
using IImageService = Web.StaticFiles.App.IImageService;

namespace Web.StaticFiles
{
    public class Startup
    {
        public AppSettings AppSettings { get; set; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            ConfigureAppSettings(services);
            
            services.AddAuthentication(JwtAuthorizationConsts.FrontendAuthenticationScheme)
                .AddJwtBearer(JwtAuthorizationConsts.FrontendAuthenticationScheme, o =>
                {
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateLifetime = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(AppSettings.SigninSecretKey.GetBytes()),
                        ValidIssuers = AppSettings.IssuerNames,
                        ValidAudiences = AppSettings.FrontendUrls
                    };
                });

            services.AddAuthorization(o =>
            {
                o.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(JwtAuthorizationConsts.FrontendAuthenticationScheme)
                    .Build();
            });
            
            services.AddHangfire(c =>
                c.UsePostgreSqlStorage(Configuration.GetConnectionString("PostgreSQL")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
                app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseHangfireServer();
            app.UseHangfireDashboard();
        }
        
        private void ConfigureAppSettings(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new Services());
        }
    }
    
    public class Services : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileSystemImageService>().As<IImageService>();
        }
    }
}