using AutoMapper;
using Domain.AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Context;
using Repository.Models;
using Service.Models;

namespace Controller
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
            services.AddCors();
            services.AddControllers();

            IServiceCollection serviceCollection = services.AddDbContext<BaseContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(AutoMapping));

            #region Repository

            services.AddScoped<ClienteRepository>();
            services.AddScoped<FilmeRepository>();
            services.AddScoped<LocacaoRepository>();

            #endregion

            #region Service

            services.AddScoped<ClienteService>();
            services.AddScoped<FilmeService>();
            services.AddScoped<LocacaoService>();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x =>
            {
                x
                .WithOrigins("http://localhost:8080")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });

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
