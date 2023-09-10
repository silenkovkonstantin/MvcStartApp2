using Microsoft.EntityFrameworkCore;
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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(_configuration);
            string? connection = _configuration.GetConnectionString("DefaultConnection"); 
            services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);
            services.AddControllersWithViews();
            services.AddSingleton<IBlogRepository, BlogRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (!_env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            // Подключаем логирование с использованием ПО промежуточного слоя
            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
