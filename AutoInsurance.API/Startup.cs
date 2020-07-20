using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoInsurance.API.Filters;
using AutoInsurance.API.Models;
using AutoInsurance.API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AutoInsurance.API
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
            services.AddDbContext<AutoInsuranceContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("AIContext")));
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAPIRequestIO",
                    builder => builder.WithOrigins("https://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<HashService>();
            services.AddTransient<IFileStorageService, InAppStorageService>();
            services.AddHttpContextAccessor();
            services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<AutoInsuranceContext>()
            .AddDefaultTokenProviders();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = false,
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(
                   Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                       ClockSkew = TimeSpan.Zero
                   }
               );
            services.AddControllers(options =>
        {
            options.Filters.Add(typeof(GlobalExceptionFilter));
        })
            .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
            .AddXmlDataContractSerializerFormatters();
services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", 
                    
                    Title = "AutoInsuranceAPI",
                    Description = "This is a Web API for AutoInsurance operations",
                    TermsOfService = new Uri("https://microsoft.com"),
                    License = new OpenApiLicense()
                    {
                        Name = "MIT"
                    },
                    Contact = new OpenApiContact()
                    {
                        Name = "Rashid Majeed",
                        Email = "rm.webdeveloper1981@gmail.com",
                        Url = new Uri("https://microsoft.com")
                    } 
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);

            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
          IApplicationBuilder app,
           IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "AutoInsuranceAPI");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
