﻿using Microsoft.EntityFrameworkCore;
using MvcStartApp2.Models.Db;
using MvcStartApp2.Middlewares;

namespace MvcStartApp2
{
    public class Startup
    {
        static IWebHostEnvironment _env;

        public IConfiguration _configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        /// <summary>
        /// Метод вызывается средой ASP.NET
        /// Используется для подключения сервисов приложения
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IBlogRepository, BlogRepository>();
            services.AddSingleton<IConfiguration>(_configuration);
            string? connection = _configuration.GetConnectionString("DefaultConnection"); 
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            services.AddControllersWithViews();
        }

        /// <summary>
        /// Метод вызывается средой ASP.NET
        /// Используется для настройки конвейра запросов
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проверяем не запущен ли проект в среде разработки
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Обрабатываем ошибки HTTP
            app.UseStatusCodePages();
            // Компонент отвечающий за маршрутизацию
            app.UseRouting();
            // Поддержка статических файлов
            app.UseStaticFiles();
            // Подключаем логирование с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            app.UseAuthorization();
            //app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}