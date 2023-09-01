using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Database.Context;
using Core.Database.Interface;
using Core.Middleware;
using Core.Model.Config;
using FilmApi.Clients;
using FilmApi.Extensions;
using FilmApi.Repository;
using FilmApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace FilmApi
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
          //  services.AddControllers().AddFluentValidation(fv=> fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddControllers();
            services.AddMySwagger();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(x =>
                {
                    x.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration["Token:Issuer"], 
                        ValidAudience =  Configuration["Token:Audience"], 
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"])), // token kodu "JwtKeyTokenKodu"
                        ClockSkew = TimeSpan.Zero,
                        RoleClaimType = "roles"// ilk kullanım
                    };
                    services.AddControllers();
                });
            
            services.AddMemoryCache();
            
            var dbSettings = Configuration.GetSection("DatabaseSettings").Get<GenericDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client,dbSettings.DatabaseName);

            services.AddScoped<IFilmService, FilmService>();
            services.AddSingleton<IContext, Context>(_ => context);
            services.AddSingleton<IFilmRepository, FilmRepository>();
           
            services.AddScoped<IOmdbHttpClient>(sp =>sp.GetRequiredService<OmdbHttpClient>());
            services.AddHttpClient<IOmdbHttpClient, OmdbHttpClient>();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmApi v1"));
            }

            app.UseMiddleware<ErrorHandlingMiddleware>();
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}