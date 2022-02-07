using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using NotificationSender.Data;
using NotificationSender.Services;
using NotificationSender.Interfaces;
using System.Text.Json.Serialization;

namespace NotificationSender
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

            services.AddControllers().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NotificationSender", Version = "v1" });
            });

            var pgPassword = Environment.GetEnvironmentVariable("PGPASSWORD");
            var pgDatabase = Environment.GetEnvironmentVariable("PGDATABASE");
            var pgPort = Environment.GetEnvironmentVariable("PGPORT");
            var pgUser = Environment.GetEnvironmentVariable("PGUSER");
            var connectionString = $"Host=host.docker.internal;Port={pgPort};Database={pgDatabase};Username={pgUser};Password={pgPassword}";
            services.AddDbContext<NotificationSenderContext>(options =>
                    options.UseNpgsql(connectionString));

            services.AddMvc();

            services.AddSingleton<IAndroidNotificationService, AndroidNotificationService>();
            services.AddSingleton<IIOSNotificationService, IOSNotificationService>();
            services.AddSingleton<ISenderResolver, SenderResolver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NotificationSender v1"));
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
