namespace FinPlus.Presentation.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class AuthController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
