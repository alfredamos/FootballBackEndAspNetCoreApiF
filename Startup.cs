using FootballBackEndAspNetCoreApiF.Contracts;
using FootballBackEndAspNetCoreApiF.Data;
using FootballBackEndAspNetCoreApiF.Mappings;
using FootballBackEndAspNetCoreApiF.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballBackEndAspNetCoreApiF
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
            services.AddCors(policy => policy.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                ));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FootballBackEndAspNetCoreApiF", Version = "v1" });
            });

            services.AddScoped<ICoachRepository, SQLCoachRepository>();
            services.AddScoped<ILeagueRepository, SQLLeagueRepository>();
            services.AddScoped<IMatchRepository, SQLMatchRepository>();
            services.AddScoped<IRefreeRepository, SQLRefreeRepository>();
            services.AddScoped<ITeamRepository, SQLTeamRepository>();
            services.AddAutoMapper(typeof(Mapping));

            services.Configure<RouteOptions>(option => {
                option.LowercaseUrls = true;
                option.LowercaseQueryStrings = true;
                option.AppendTrailingSlash = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FootballBackEndAspNetCoreApiF v1"));
            }

            app.UseCors("CorsPolicy");

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
