namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Trafer;
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using FinPlusService.DefualtBet;
    using Microsoft.AspNetCore.Mvc;

    public class TrafferController : Controller
    {
        private readonly ITraferService _trafferService;
        private readonly IDefaultBetService _defaultBetService;

        public TrafferController(ITraferService trafferService, IDefaultBetService defaultBetService)
        {
            _trafferService = trafferService;
            _defaultBetService = defaultBetService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            List<TrafferModel> model = new List<TrafferModel>();
            var traffers = await _trafferService.GetAllTraffers();

            foreach (var traffer in traffers)
            {
                TrafferModel traferModel = new TrafferModel()
                {
                    Id = traffer.Id,
                    Name = traffer.Name,
                    Login = traffer.Login,
                    Password = traffer.Password,
                    ReferalId = traffer.ReferalId,
                    MobileNumber = traffer.MobileNumber,
                    Level = traffer.Level,
                    Bet = traffer.Bet,
                    Revenue = await _trafferService.GetTrafferRevenue(traffer.Id),
                    ProfitInMonth = await _trafferService.GetTrafferProfitInMonth(traffer.Id, DateTime.Now.Month),
                };

                model.Add(traferModel);
            }

            return View(model);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> AddTraffer(TrafferModel model, string name)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (name == null)
            {
                return View(model);
            }

            var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                return BadRequest("Введите полное ФИО");
            }

            var fio = new FIO
            {
                Surname = parts[0],
                Name = parts[1],
                Patronymic = parts.Length > 2 ? parts[2] : string.Empty,
            };

            model.ReferalId = "001";
            string orgId = "000";
            TrafferBet bet = new TrafferBet()
            {
                Bet = await _defaultBetService.GetDefaultBetByOrganisationId(orgId),
            };

            Traffer trafer = new Traffer()
            {
                Name = fio,
                Login = model.Login,
                Password = model.Password,
                ReferalId = model.ReferalId,
                MobileNumber = model.MobileNumber,
                Level = model.Level,
                Bet = bet,
            };

            await _trafferService.AddTraffer(trafer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditTraffer(string id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var traffer = await _trafferService.GetTrafferById(id);
            return View(traffer);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTraffer(Traffer traffer, string name)
        {
            if (ModelState.IsValid)
            {
                var parts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2)
                {
                    return BadRequest("Введите полное ФИО");
                }

                var fio = new FIO
                {
                    Surname = parts[0],
                    Name = parts[1],
                    Patronymic = parts.Length > 2 ? parts[2] : string.Empty,
                };
                traffer.Name = fio;
                await _trafferService.UpdateTraffer(traffer);
                return RedirectToAction("Index");
            }

            return View("EditTraffer", traffer);
        }

        public async Task<IActionResult> SearchTraffer(string partName)
        {
            var traffers = await _trafferService.GetTrafferByPartName(partName);
            List<TrafferModel> model = new List<TrafferModel>();

            foreach (var traffer in traffers)
            {
                TrafferModel trafferModel = new TrafferModel()
                {
                    Id = traffer.Id,
                    Name = traffer.Name,
                    Login = traffer.Login,
                    Password = traffer.Password,
                    ReferalId = traffer.ReferalId,
                    MobileNumber = traffer.MobileNumber,
                    Level = traffer.Level,
                    Bet = traffer.Bet,
                    Revenue = await _trafferService.GetTrafferRevenue(traffer.Id),
                    ProfitInMonth = await _trafferService.GetTrafferProfitInMonth(traffer.Id, DateTime.Now.Month),
                };

                model.Add(trafferModel);
            }

            return View("Index", model);
        }
    }
}