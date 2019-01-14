using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Shop.Services.Interfaces;
using Shop.Infrastructure.Business.Services;
using Shop.Api.Middleware;
using FluentValidation.AspNetCore;
using Shop.Api.Filters;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Shop.Api.Extensions;

namespace Shop.Api
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
            services.AddDbContextPool<ApplicationDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Shop")));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IBrandsService, BrandsService>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IAttributesService, AttributesService>();
            services.AddTransient<IPersonsService, PersonsService>();
            services.AddTransient<IOrdersService, OrdersService>();

            var authorization = Configuration.GetAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = authorization.Issuer,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = authorization.Audience,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = authorization.SecurityKey,
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddMvc(opt => opt.Filters.Add<ValidatorActionFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandling()
               .UseStatusCodePagesWithReExecute("/Errors/{0}")
               .UseAuthentication()
               .UseMvc();
        }
    }
}
