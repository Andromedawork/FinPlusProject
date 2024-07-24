namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Offers;
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using Microsoft.AspNetCore.Mvc;

    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;

        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            List<OfferModel> listOffers = new List<OfferModel>();
            var offers = await _offerService.GetAllOffers();
            foreach (var offer in offers)
            {
                OfferModel model = new OfferModel()
                {
                    Name = offer.Name,
                    Type = offer.Type,
                    TargetAction = offer.TargetAction,
                    Profit = offer.Profit,
                };

                listOffers.Add(model);
            }

            return View(listOffers);
        }

        public async Task<IActionResult> AddOffer(Offer model)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (model.Name == null)
            {
                return View(model);
            }

            await _offerService.AddOffer(model);

            return RedirectToAction("Index");
        }
    }
}
