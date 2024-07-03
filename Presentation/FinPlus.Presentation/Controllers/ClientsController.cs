namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Presentation.Models;
    using FinPlusService;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;

    public class ClientsController : Controller
    {
        private readonly ILogger<ClientsController> _logger;

        private readonly IDropService _dropService;

        public ClientsController(ILogger<ClientsController> logger, IDropService dropService)
        {
            _logger = logger;
            _dropService = dropService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var drops = _dropService.GetAllDrops();
                List<DropModel> model = new List<DropModel>();
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
        public async Task<IActionResult> AddClient(DropModel model)
        {
            if (model.Name != null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public async Task<IActionResult> EditClient(DropModel model)
        {
            return View();
        }
    }
}