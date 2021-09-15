using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MrSupportV2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MrSupportV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Gettem(String SearchContent)
        {
            System.Threading.Thread.Sleep(2000);
            string message = $"Searching for {SearchContent}";
            var retval = Json(new { message = message });
            return retval;
        }

        public IActionResult ErrorList()
        {
            return View(GetErrorDetais());
        }

        public IEnumerable<ErrorDetailViewModel> GetErrorDetais()
        {
            List<ErrorDetailViewModel> edvms = new List<ErrorDetailViewModel>();
            ErrorDetailViewModel edvm = new ErrorDetailViewModel()
            {
                ErrorCode = "XXXX-XXXX",
                User = "Hugh Janus",
                Time = DateTime.Now.AddDays(-3),
                Error = "Object reference not equal to instance ...",
                StackTrace = "at"
            };
            ErrorDetailViewModel edvm2 = new ErrorDetailViewModel()
            {
                ErrorCode = "XXXX-YYYY",
                User = "Ivor Biggun",
                Time = DateTime.Now.AddDays(-2),
                Error = "File not found",
                StackTrace = "at"
            };
            edvms.Add(edvm);
            edvms.Add(edvm2);
            return edvms;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Flubble
    {
        public string Fibble { get; set; }
        public string Fobble { get; set; }
    }
}
