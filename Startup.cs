using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Spartan.Interfaces;
using Spartan.Options;
using Spartan.Persistence.Contexts;
using Spartan.Persistence.Repositories;
using Spartan.Services;

namespace Spartan
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(options =>
                        //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                        options.UseInMemoryDatabase("someDB")
                        .EnableDetailedErrors(true));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                            .GetBytes(Configuration.GetSection("AppSetting:Token").Value)),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            services.AddCors();

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddSwaggerGen(x =>
                x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Spartan API", Description = "Available Contracts", Version = "v1" })
            );
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseHsts();
            }

            var sOptions = new SwaggerOptions();
            Configuration.GetSection(nameof(SwaggerOptions)).Bind(sOptions);
            app.UseSwagger();
            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint(sOptions.UiEndpoint, sOptions.Description);
            });

            app.UseCors(c => c.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();

            app.Run(async context =>
               {
                   await context.Response.WriteAsync("Hello there, welcome to spartan.");
               });
        }
    }
}
