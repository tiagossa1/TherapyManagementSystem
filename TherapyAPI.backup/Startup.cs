using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TherapyAPI.Entities;
using TherapyAPI.Helpers;
using TherapyAPI.Mapper;
using TherapyAPI.Repository;
using TherapyAPI.Repository.Base.Interface;

namespace TherapyAPI
{
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            var connection = Configuration["ConnectionStrings:Sqlite"];

            services.AddDbContext<RepositoryContext> (options => {
                options.UseSqlite (connection);
            });

            var config = new MapperConfiguration (cfg => {
                cfg.AddProfile<ModelToDtoMapper> ();
            });

            services.AddCors ();
            // services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2)
            //     .AddJsonOptions (options => {
            //         options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
            //     });
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Latest);
            services.AddAutoMapper ();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection ("AppSettings");
            services.Configure<AppSettings> (appSettingsSection);
            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings> ();
            var key = Encoding.ASCII.GetBytes (appSettings.Secret);
            //services.AddAuthentication (x => {
            //        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer (x => {
            //        x.RequireHttpsMetadata = false;
            //        x.SaveToken = true;
            //        x.TokenValidationParameters = new TokenValidationParameters {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey (key),
            //            ValidateIssuer = false,
            //            ValidateAudience = false
            //        };
            //    });

            // configure DI for application services
            services.AddScoped<ITherapistRepository, TherapistRepository> ();
            services.AddScoped<IClientRepository, ClientRepository> ();
            services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository> ();
            services.AddScoped<IBillingRepository, BillingRepository> ();
            services.AddScoped<IAppointmentRepository, AppointmentRepository> ();

            services.AddSwaggerGen (c => {
                c.SwaggerDoc ("v1", new Info { Title = "Therapy API", Version = "v1" });
                c.AddSecurityDefinition ("Bearer", new ApiKeyScheme { In = "header", Description = "Please enter Jwt with Bearer into field", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement (new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string> () },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
            }

            //app.UseHttpsRedirection();
            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ());

            app.UseAuthentication ();

            app.UseMvc ();
            app.UseSwagger ();
            app.UseSwaggerUI (c => {
                c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Therapy API");
            });
        }
    }
}