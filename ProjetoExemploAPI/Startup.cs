using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjetoExemploAPI.Context;
using ProjetoExemploAPI.Model;
using ProjetoExemploAPI.repositories;
using ProjetoExemploAPI.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoExemploAPI
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
            services.AddControllers();

            var connection = @"Server=(localdb)\mssqllocaldb;Database=AspCore_NovoDB;Trusted_Connection=True;";
            services.AddDbContext<MyContext>(options => options.UseSqlServer(connection));

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Area Vendedor API",
                    Version = "v1",
                    Description = "API da Area Vendedor",
                    Contact = new OpenApiContact
                    {
                        Name = "Ti Technos",
                        Email = "sistemas@grupotechnos.com.br",
                    }
                });
            });
            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ILogger, LoggerService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
            });

            IMapper mapper = config.CreateMapper();

            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(option =>
            {
                option.SwaggerEndpoint("/swagger/v1/swagger.json", "Area Vendedor API V1 " + env.EnvironmentName);
                option.RoutePrefix = "swagger";
                option.DocExpansion(DocExpansion.None);
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
