using CorrelationId;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using TCloud.Api.Filters;
using TCloud.Api.Models;

namespace TCloud.Api
{
    public class Startup
    {
        private readonly IConfiguration config;

        public Startup(IConfiguration config, IHostEnvironment env)
        {
            this.config = config;
        }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(p => new MongoClient(config.GetConnectionString("TCloud")));
            services.AddSingleton(p => p.GetService<IMongoClient>().GetDatabase("tcloud"));
            services.AddSingleton(p => p.GetService<IMongoDatabase>().GetCollection<Movie>("movies"));
            services.AddRouting();
            services.AddControllers(cfg => { cfg.Filters.Add<ValidateModelFilterAttribute>(); });
            services.AddCorrelationId();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCorrelationId();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(cfg => { cfg.MapControllers(); });
        }
    }
}
