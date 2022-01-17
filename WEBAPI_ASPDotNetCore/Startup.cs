using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI_ASPDotNetCore.Contracts;
using WEBAPI_ASPDotNetCore.Models;
using WEBAPI_ASPDotNetCore.Repository;

namespace WEBAPI_ASPDotNetCore
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


            //services.AddDbContext<Models.EmployeeContext>(opt => opt.UseInMemoryDatabase("CustomerInfo"));
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.Configure<FormOptions>(o =>
            { o.ValueLengthLimit = int.MaxValue;
                  o.MultipartBodyLengthLimit = int.MaxValue;
                  o.MemoryBufferThreshold = int.MaxValue;
            });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IImageReposWrapper, ImageReposWrapper>();
            services.AddTransient<IAdd, Add>();
            services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connectionstring")));

            services.AddControllers();
            var jwtSettings = Configuration.GetSection("JwtSettings");
            var a = jwtSettings.GetSection("validIssuer").Value;
            /* JSON WEB Token (JWT) Authentication for Login */
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                    ValidAudience = jwtSettings.GetSection("validAudience").Value,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("Secret").Value))
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WEBAPI_ASPDotNetCore", Version = "v1" });
            });
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBAPI_ASPDotNetCore v1"));
            }
            app.UseCors("MyPolicy");
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"Resources")),
                RequestPath = new PathString("/Resources")
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
