using Diginet.Domain.Data.Db;
using Diginet.Domain.Helpers.Concrete;
using Diginet.Domain.Helpers.Interface;
using Diginet.Domain.Models.DTOs.Concrete;
using Diginet.Domain.Repositories.Concrete;
using Diginet.Domain.Repositories.Interface;
using Diginet.Domain.Services;
using Diginet.Domain.Services.Interface;
using Diginet.Web.API.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diginet.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Diginet.Web.API", Version = "v1" });
                c.OperationFilter<CustomHeaderFilters.AddRequiredHeaderParameter>();
            });

            // Register Code First Migrations Dependencies
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
            Configuration.GetConnectionString("DiginetConnection"), b => b.MigrationsAssembly("Diginet.Domain")));

            // Register Dapper Dependencies
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Register Repositories
            services.AddScoped<IOrderRepo, OrderRepository>();

            //Register Services
            services.AddScoped<IOrderService, OrderService>();

            // Register Helpers
            services.AddScoped<IUtil, Util>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diginet.Web.API v1"));

                //app.UseSwaggerUI(c =>
                //{
                //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Diginet.Web.API v1");
                //    c.RoutePrefix = "api/controller";

                //});
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
