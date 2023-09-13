using MvcStartApp2.Models.Repository;
using RequestLibrary;

namespace MvcStartApp2.Middlewares
{
    public class LoggingMiddleware
    {
        private IWebHostEnvironment _env;
        private readonly RequestDelegate _next;
        private IRequestRepository _log;

        /// <summary>
        ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
        /// </summary>
        public LoggingMiddleware(RequestDelegate next, IWebHostEnvironment env, IRequestRepository log)
        {
            _next = next;
            _env = env;
            _log = log;
        }

        /// <summary>
        /// Метод записи логов
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task WriteRequestLogIntoTXT(HttpContext context)
        {
            // Для логирования данных о запросе используем свойста объекта HttpContext
            string logMessage = $"[{DateTime.Now}]: New request to http://{context.Request.Host.Value + context.Request.Path}" + Environment.NewLine;

            // Путь до лога (опять-таки, используем свойства IWebHostEnvironment)
            string logFilePath = Path.Combine(_env.ContentRootPath, "Logs", "RequestLog.txt");

            // Асинхронная запись в файл RequestLog.txt
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }

        /// <summary>
        /// Метод вывода логов в консоль
        /// </summary>
        /// <param name="context"></param>
        private void LogConsole(HttpContext context)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]: New request to http://{context.Request.Host.Value + context.Request.Path}");
        }

        private async Task LogDb(HttpContext context)
        {
            await File.AppendAllTextAsync(Path.Combine(_env.ContentRootPath, "Logs", "RequestLog.txt"), $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}]: New request to http://{context.Request.Host.Value + context.Request.Path}" + Environment.NewLine);
        }

        /// <summary>
        ///  Необходимо реализовать метод Invoke  или InvokeAsync
        /// </summary>
        public async Task InvokeAsync(HttpContext context)
        {
            LogConsole(context);

            await WriteRequestLogIntoTXT(context);
            await LogDb(context);
            // Передача запроса далее по конвейеру
            await _next.Invoke(context);
        }
    }
}