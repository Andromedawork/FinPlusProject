namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Drop;
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using FinPlusService.Sources;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;

    public class ClientController : Controller
    {
        private readonly ILogger<ClientController> _logger;

        private readonly IDropService _dropService;

        private readonly ITraferService _traferService;

        private readonly ISourceService _sourceService;

        private readonly IOfferService _offerService;

        public ClientController(ILogger<ClientController> logger, IDropService dropService, IOfferService offerService, ITraferService traferService, ISourceService sourceService)
        {
            _logger = logger;
            _dropService = dropService;
            _offerService = offerService;
            _sourceService = sourceService;
            _traferService = traferService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            try
            {
                var drops = await _dropService.GetAllDrops();
                List<DropModel> model = new List<DropModel>();
                foreach (var drop in drops)
                {
                    DropModel dropModel = new DropModel()
                    {
                        Id = drop.Id,
                        Name = drop.Name.ToString(),
                        Partner = await _dropService.GetPartner(drop.ReferalId),
                        SubPartner = await _dropService.GetSubPartner(drop.ReferalId),
                        Source = await _dropService.GetPartner(drop.ReferalId),
                        MobileNumber = drop.MobileNumber,
                        Telegram = drop.Telegram,
                        Pass = drop.Pass.ToString(),
                        CardNumber = drop.CardNumber,
                        DateOfBirth = DateTime.SpecifyKind(drop.DateOfBirth, DateTimeKind.Utc).ToLocalTime(),
                        Date = DateTime.Parse(drop.Steps.Keys.First()),
                        ProfitPotencial = drop.ProfitPotencial,
                        RevenuePotential = drop.RevenuePotential,
                    };
                    model.Add(dropModel);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Ошибка при получение списка клиентов с Базы");
                ErrorViewModel error = new ErrorViewModel(404);
                return RedirectToAction("Error", "Home", error);
            }
        }

        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> AddClient(DropModel model, string comment)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (model.Name != null)
            {
                var parts = model.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

                parts = model.Pass.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var pass = new DropPassport
                {
                    Series = parts[0],
                    Number = parts[1],
                };

                var orgId = HttpContext.Session.GetString("OrganisationId");
                model.Comment.Add(comment);

                Dictionary<string, List<Offer>> offers = new Dictionary<string, List<Offer>>()
                {
                    {
                        DropStep.FirstStep.ToString(), await _offerService.GetAllOffersById(model.Offers)
                    },
                };

                Drop drop = new Drop()
                {
                    Name = fio,
                    Pass = pass,
                    MoneyStatus = false,
                    MobileNumber = model.MobileNumber,
                    Telegram = model.Telegram,
                    DateOfBirth = DateTime.SpecifyKind(model.DateOfBirth, DateTimeKind.Utc).ToLocalTime(),
                    OrganisationId = orgId,
                    Offers = offers,
                    Comments = model.Comment,
                    Steps = new Dictionary<string, DropStep> { { DateTime.Now.ToString(), DropStep.FirstStep } },
                    CardNumber = model.CardNumber,
                    ReferalId = model.Partner,
                };

                await _dropService.AddDrop(drop);
                return RedirectToAction("Index");
            }

            model.AllTraffers = await _traferService.GetAllTraffers();
            model.AllOffers = await _offerService.GetAllOffers();
            model.AllSources = await _sourceService.GetAllSources();
            return View(model);
        }

        public async Task<IActionResult> EditClient(DropModel model)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;
            var drop = await _dropService.GetDropById(model.Id);

            DropModel dropModel = new DropModel()
            {
                Name = drop.Name.ToString(),
                Partner = await _dropService.GetPartner(drop.ReferalId),
                SubPartner = await _dropService.GetSubPartner(drop.ReferalId),
                Source = await _dropService.GetPartner(drop.ReferalId),
                MobileNumber = drop.MobileNumber,
                Telegram = drop.Telegram,
                Pass = drop.Pass.ToString(),
                CardNumber = drop.CardNumber,
                DateOfBirth = DateTime.SpecifyKind(drop.DateOfBirth, DateTimeKind.Utc).ToLocalTime(),
                Date = DateTime.Parse(drop.Steps.Keys.First()),
                ProfitPotencial = drop.ProfitPotencial,
                RevenuePotential = drop.RevenuePotential,
                ReceivedOffers = drop.Offers.Values.SelectMany(offerList => offerList).ToList(),
                Comment = drop.Comments,
            };
            dropModel.AllTraffers = await _traferService.GetAllTraffers();
            dropModel.AllOffers = await _offerService.GetAllOffers();
            dropModel.AllSources = await _sourceService.GetAllSources();

            return View(dropModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateClientData(DropModel model)
        {
            if (model.Name != null)
            {
                var parts = model.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

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

                parts = model.Pass.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                var pass = new DropPassport
                {
                    Series = parts[0],
                    Number = parts[1],
                };

                await _dropService.UpdateDropData(model.Id, fio, model.Partner, pass, model.MobileNumber, model.CardNumber, model.PersonalReferalId, model.DateOfBirth, model.Telegram);
            }

            return RedirectToAction("EditClient", model);
        }

        public async Task<IActionResult> UpdateClientStep(DropModel model, int step)
        {
            var offers = await _offerService.GetAllOffersById(model.Offers);
            if (await _dropService.UpdateDropStep(model.Id, step, offers, model.Comment[0]))
            {
                return RedirectToAction("EditClient", model);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchClient(string partName)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            List<DropModel> model = new List<DropModel>();
            var drops = await _dropService.GetAllDropsByPartName(partName);
            return View("Index", model);
        }
    }
}