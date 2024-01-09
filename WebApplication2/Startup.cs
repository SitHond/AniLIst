using API.Controllers;
using API.interfaces;
using API.Repositories;
using Microsoft.OpenApi.Models;

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

            // Добавление службы распределенного кэша в памяти
            services.AddDistributedMemoryCache();

            // Добавление службы сессий
            services.AddSession(options =>
            {
                options.Cookie.Name = ".AniList.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(30); // Пример времени ожидания сессии
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
#if DEBUG
                        builder.WithOrigins("http://localhost:8080")
#else
                            builder.WithOrigins("http://sithond.ru")
#endif
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
                    Description = "API Description",
                });
                var filePath = Path.Combine(AppContext.BaseDirectory, "site.xml");
                c.IncludeXmlComments(filePath);
            });

            services.AddDbContext<AppDbContext>();
            services.AddHealthChecks();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAnimeRepository, AnimeRepository>();
            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseSwagger(opt =>
            {
                opt.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                opt.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseHealthChecks("/health");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
