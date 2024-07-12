namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Presentation.Models;
    using Microsoft.AspNetCore.Mvc;

    public class OfferController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            List<OfferModel> model = new List<OfferModel>();
            return View(model);
        }

        public async Task<IActionResult> AddOffer(OfferModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            return View(model);
        }
    }
}
