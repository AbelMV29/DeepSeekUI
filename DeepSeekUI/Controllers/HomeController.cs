using DeepSeekUI.Domain.Entities;
using DeepSeekUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DeepSeekUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpGet()]
        public IActionResult Index([FromQuery] Guid? chat = null)
        {
            if(!User.Identity.IsAuthenticated && chat is not null)
            {
                return RedirectToAction(actionName: "Login", controllerName: "Auth");
            }

            ViewBag.ChatId = chat;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
