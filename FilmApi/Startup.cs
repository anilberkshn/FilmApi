using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Core.Database.Context;
using Core.Database.Interface;
using Core.Middleware;
using Core.Model.Config;
using FilmApi.Clients;
using FilmApi.Repository;
using FilmApi.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmApi", Version = "v1" }); });
            
            
            var dbSettings = Configuration.GetSection("DatabaseSettings").Get<GenericDatabaseSettings>();
            var client = new MongoClient(dbSettings.ConnectionString);
            var context = new Context(client,dbSettings.DatabaseName);

            services.AddScoped<IFilmService, FilmService>();
            services.AddSingleton<IContext, Context>(_ => context);
            services.AddSingleton<IFilmRepository, FilmRepository>();
           
            services.AddScoped<IOmdbHttpClient>(sp =>sp.GetRequiredService<OmdbHttpClient>());
            services.AddHttpClient<IOmdbHttpClient, OmdbHttpClient>();
            // services.AddHttpClient<IOmdbHttpClient>((sp, http) =>
            // {
            //     http.BaseAddress = new Uri("http://www.omdbapi.com/");
            //     http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}