using API.Controllers;
using API;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using Umbraco.Core.Services.Implement;

namespace site

{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Настройка подключения к базе данных MySQL
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

            // Другие настройки сервисов
        }
    }
}
