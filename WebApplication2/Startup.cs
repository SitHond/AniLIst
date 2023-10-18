using API.Controllers;
using API.interfaces;
using API.Repositories;
using Microsoft.OpenApi.Models;
using Swashbuckle;

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
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "API",
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "site.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddDbContext<AppDbContext>(); // Здесь подставьте ваш контекст базы данных
            services.AddScoped<IAnimeRepository, AnimeRepository>(); // Здесь подставьте ваши сервисы и репозитории
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(); // Важно добавить это перед UseRouting()

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                opt.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
