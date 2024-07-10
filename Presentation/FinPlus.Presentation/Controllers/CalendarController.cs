namespace FinPlus.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
