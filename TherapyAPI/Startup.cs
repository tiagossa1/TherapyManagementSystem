using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TherapyAPI.Context;
using TherapyAPI.Mapper;
using TherapyAPI.Models;
using TherapyAPI.Repository;
using TherapyAPI.Repository.Base.Interface;
using TherapyAPI.Validators;

namespace TherapyAPI
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
            var connection = Configuration["ConnectionStrings:Sqlite"];

            services.AddDbContext<RepositoryContext>(options => options.UseSqlite(connection));

            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperProfile()));

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // --- Dependency Injection below ---
            // -----------------------------------------------------------------------------
            // Repositories
            services.AddScoped<ITherapistRepository, TherapistRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IAppointmentTypeRepository, AppointmentTypeRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IGenderRepository, GenderRepository>();
            services.AddScoped<ICivilStatusRepository, CivilStatusRepository>();

            // Validators
            services.AddScoped<IValidator<AppointmentType>, AppointmentTypeValidator>();
            services.AddScoped<IValidator<Appointment>, AppointmentValidator>();
            services.AddScoped<IValidator<Billing>, BillingValidator>();
            services.AddScoped<IValidator<CivilStatus>, CivilStatusValidator>();
            services.AddScoped<IValidator<Client>, ClientValidator>();
            services.AddScoped<IValidator<Gender>, GenderValidator>();
            services.AddScoped<IValidator<Therapist>, TherapistValidator>();
            // -----------------------------------------------------------------------------

            services.AddControllers();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "TherapyAPI", Version = "v1" }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication1 v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}