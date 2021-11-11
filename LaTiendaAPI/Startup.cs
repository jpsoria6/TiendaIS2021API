using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LaTienda.API;
using LaTienda.API.Controllers;
using LaTienda.API.Features.Productos;
using LaTienda.API.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LaTiendaAPI
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

            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.ToString());
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LaTiendaAPI", Version = "v1" });
            });


            ConfigureDB(services);
            ConfigureMediatR(services);
            ConfigureMediatR(services);
            ConfigureMapper(services);

        }

        private void ConfigureMapper(IServiceCollection service)
        {
            var mapperConfig = new MapperConfiguration(m =>
            {
                m.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }

        private void ConfigureMediatR(IServiceCollection services)
        {
            services.AddMediatR(typeof(ColorsController));
            services.AddMediatR(typeof(MarcasController));
            services.AddMediatR(typeof(TallesController));
            services.AddMediatR(typeof(UsersController));
            services.AddMediatR(typeof(RubrosController));
            services.AddMediatR(typeof(StockController));
            services.AddMediatR(typeof(VentasController));
            services.AddMediatR(typeof(GetProductoByIdQuery));

        }
        
        private void ConfigureDB(IServiceCollection services)
        {
            var connString = Configuration.GetConnectionString("DefaultConnectionString");
            services.AddDbContext<TiendaContext>(opts =>
            {
                System.Diagnostics.Debug.WriteLine(Configuration.GetConnectionString(connString));
                opts.UseSqlServer(connString);
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LaTiendaAPI v1"));
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
