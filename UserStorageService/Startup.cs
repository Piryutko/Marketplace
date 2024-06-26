using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
using UserStorageService.AsyncDataServices;
using UserStorageService.Data;
using UserStorageService.Event;
using UserStorageService.Facades;
using UserStorageService.GRPC;
using UserStorageService.Interfaces;
using UserStorageService.Repositories;

namespace UserStorageService
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
            
            services.AddDbContext<AppDbContext>(opt =>
             opt.UseInMemoryDatabase("InMem"));

            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddHostedService<MessageBusSubscriber>();

            services.AddGrpc();

            services.AddSingleton<IEventProcessor, EventProcessor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserFacade, UserFacade>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserStorageService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserStorageService v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGrpcService<UserRegistrationService>();

                endpoints.MapGet("/Protos/validateuser.proto", async context =>
                {
                    await context.Response.WriteAsync(File.ReadAllText("Protos/validateuser.proto"));
                });
            });
        }
    }
}
