using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BuddhaTerapias_API.Interfaces;
using BuddhaTerapias_API.Models;
using BuddhaTerapiasAPI.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace BuddhaTerapias_API {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<RepositoryContext> (options =>
                options.UseSqlite (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2)
                .AddJsonOptions (options => {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
                });
            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info { Title = "Buddha Terapias API", Version = "v1" });
            });
            services.AddScoped<IClientRepository, ClientRepository> ();
            services.AddScoped<IDevRepository, DevRepository> ();
            services.AddScoped<ITherapistRepository, TherapistRepository> ();
            Mapper.Initialize (x => {
                x.AddProfile<ClientProfile> ();
            });

            Mapper.Configuration.AssertConfigurationIsValid ();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //app.UseHttpsRedirection();
            app.UseMvc ();
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "BuddhaTerapiasAPI");
            });

        }
    }
}