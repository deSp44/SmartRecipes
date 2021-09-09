using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartRecipesMVC.Web.Services.Models;

namespace SmartRecipesMVC.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (HttpContext.Connection.RemoteIpAddress != null)
            {
                var ip = HttpContext.Connection.RemoteIpAddress.ToString();
                _logger.LogInformation("User entered website from IP:" + ip);
            }
            _logger.LogInformation("User entered website");

            if (User.Identity is { IsAuthenticated: true })
                return RedirectToAction("Index", "Recipes");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult About()
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
