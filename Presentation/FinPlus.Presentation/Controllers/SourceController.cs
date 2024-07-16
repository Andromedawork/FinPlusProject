namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.Sources;
    using FinPlus.Presentation.Models;
    using FinPlusService.Sources;
    using Microsoft.AspNetCore.Mvc;

    public class SourceController : Controller
    {
        private readonly ISourceService _sourceService;

        public SourceController(ISourceService sourceService)
        {
            _sourceService = sourceService;
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            List<SourceModel> model = new List<SourceModel>();
            var sources = await _sourceService.GetAllSources();
            foreach (var source in sources)
            {
                SourceModel sourceModel = new SourceModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ProfitInMonth = await _sourceService.GetSoureProfitInMonth(source.Id, DateTime.MinValue, DateTime.MaxValue),
                    Revenue = await _sourceService.GetSoureRevenue(source.Id),
                    MyDrops = await _sourceService.GetAllMyDrops(source.ReferalId),
                };
                model.Add(sourceModel);
            }

            return View(model);
        }

        public async Task<IActionResult> AddSource(Source source)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            if (source.Name == null)
            {
                return View(source);
            }

            source.ReferalId = "100";
            source.OrganizationId = "000";

            await _sourceService.AddSource(source);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditSource(string id)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var source = await _sourceService.GetSourceById(id);
            return View(source);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSource(Source source)
        {
            if (ModelState.IsValid)
            {
                await _sourceService.UpdateSource(source);
                return RedirectToAction("Index");
            }

            return View("EditSource", source);
        }

        public async Task<IActionResult> SearchSource(string partName)
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            var sources = await _sourceService.GetAllSourceByPartName(partName);
            List<SourceModel> model = new List<SourceModel>();

            foreach (var source in sources)
            {
                SourceModel sourceModel = new SourceModel()
                {
                    Id = source.Id,
                    Name = source.Name,
                    ProfitInMonth = await _sourceService.GetSoureProfitInMonth(source.Id, DateTime.MinValue, DateTime.MaxValue),
                    Revenue = await _sourceService.GetSoureRevenue(source.Id),
                    MyDrops = await _sourceService.GetAllMyDrops(source.ReferalId),
                };
                model.Add(sourceModel);
            }

            return View("Index", model);
        }
    }
}
