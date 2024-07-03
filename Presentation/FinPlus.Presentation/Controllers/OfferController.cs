namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Presentation.Models;
    using Microsoft.AspNetCore.Mvc;

    public class OfferController : Controller
    {
        public IActionResult Index()
        {
            List<OfferModel> model = new List<OfferModel>();
            return View(model);
        }

        public async Task<IActionResult> AddOffer(OfferModel model)
        {
            return View(model);
        }
    }
}
