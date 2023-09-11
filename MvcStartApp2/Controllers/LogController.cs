using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MvcStartApp2.Models.Repository;

namespace MvcStartApp2.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogRepository _log;

        public LogController(ILogRepository log)
        {
            _log = log;
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _log.GetRequests();
            return View(requests);
        }
    }
}
