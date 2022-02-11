using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjetoExemploAPI.Context;
using ProjetoExemploAPI.Model.Authentication;
using ProjetoExemploAPI.Model.Produtos;
using ProjetoExemploAPI.repositories;
using ProjetoExemploAPI.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Security.Claims;
using System.Text;
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
            services.AddSingleton<IConfiguration>(Configuration);

            IdentityModelEventSource.ShowPII = true;

            var connection = @"Server=(localdb)\mssqllocaldb;Database=AspCore_NovoDB;Trusted_Connection=True;";
            services.AddDbContext<MyContext>(options => options.UseSqlServer(connection));

            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Cicero Junior API",
                    Version = "v1",
                    Description = "API de teste",
                    Contact = new OpenApiContact
                    {
                        Name = "Cicero JR",
                        Email = "ciceronascimentu@gmail.com",
                    }
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Autorização JWT Header usando scheme Bearer."
                });
            });
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ILogger, LoggerService>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, Produto>()
                .ForMember(x => x.ProdutoId, opt => opt.Ignore());
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
            option.SwaggerEndpoint("/swagger/v1/swagger.json", "Cicero Junior API");
            option.RoutePrefix = "swagger";
            option.DocExpansion(DocExpansion.None);
        });

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

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
