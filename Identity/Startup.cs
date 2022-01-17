using Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity
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
            // Appsettings
            services.Configure<ApplicatiomSetting>(Configuration.GetSection("JWTSettings"));
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connectionstring")));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity", Version = "v1" });
            });
            services.AddDefaultIdentity<ApplicationUser>()
              .AddEntityFrameworkStores<AuthenticationContext>();

            // Customization Password validation
            services.Configure<IdentityOptions>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequiredLength = 4;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequireUppercase = false;
                Options.Password.RequireLowercase = false;
            });
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
            

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity v1"));
            }
            app.UseCors("MyPolicy");
            
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
