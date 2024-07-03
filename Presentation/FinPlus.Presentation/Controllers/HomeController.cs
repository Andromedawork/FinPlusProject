namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Infrastructure;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel model)
        {
            return View(model);
        }
    }
}