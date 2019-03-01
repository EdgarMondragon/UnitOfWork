using DryIoc;
using DryIoc.Microsoft.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;

using UserSettings.API.Repositories;
using Repositories;
using UserSettings.API.App_Start;
using UserSettings.API.UnitOfWork;
using DataAccess.Infraestructure;
using GodSharp.Data.Common.DbProvider;
using System.Reflection;
using System.Linq;
using UserSettings.API.Filters;
using Microsoft.AspNetCore.Identity;
using UserSettings.API.Authorization;

namespace UserSettings.API
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            app.UseHttpsRedirection();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Settings API V1");
            });

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
            );
            app.UseMvc();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "User Settings API", Version = "v1" });
            });            
            services.AddMvc(con => {
                con.Filters.Add(new GlobalFilter());
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // Enables controllers to be resolved by DryIoc, OTHERWISE resolved by infrastructure
                .AddControllersAsServices();
            services.AddScoped<AuthorizationFilter>();
            //services.AddIdentityCore<IdentityUser>();
            services.AddCors();
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            var config = builder.Build();
            DbProviderManager.LoadConfiguration(config);
            services.AddSingleton<IConfiguration>(config);
            // DryIOC            
            var container = new Container().WithDependencyInjectionAdapter(services);
            container.Register<IApplicationContext, ApplicationContext>();
            container.Register<IUserSettings, UserSettingsMemoryRepo>();
            container.Register<IConnectionFactory, ConnectionFactory>();
            container.Register<IUnitOfWork, UnitOfWork.UnitOfWork>();
            container.Register<IUserSettingsRepository, UserSettingsRepository>();
            container.Register<IOnDutiesRepository, OnDutiesRepository>();
            container.Register<IUserSessionInfoRepository, UserSessionInfoRepository>();
            container.Register<IMemberPreferences, MemberPreferencesRepository>();
            container.Register<IResponderRepository, ResponderRepository>();
            var serviceProvider = container.Resolve<IServiceProvider>();
            // Automapper Configuration
            AutoMapperConfiguration.Configure();

            return serviceProvider;
        }
    }
}
