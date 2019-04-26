using AutoMapper;
using CompanyName.ServiceName.Api.Features;
using CompanyName.ServiceName.Api.Features.MeasurementPoints;
using CompanyName.ServiceName.DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace CompanyName.ServiceName.Api
{
    internal sealed class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _env = env ?? throw new ArgumentNullException(nameof(env));
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfile));

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
                if (_env.IsDevelopment())
                    options.EnableSensitiveDataLogging();
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<AddMeasurementPointValidator>();
                    cfg.ImplicitlyValidateChildProperties = true;
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.EnableAnnotations();
                c.AddFluentValidationRules();
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            AutoMapper.IConfigurationProvider autoMapper)
        {
            autoMapper.AssertConfigurationIsValid();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));

            app.UseMvc();
        }
    }
}
