namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.CalendarOfDrops;
    using FinPlus.Presentation.Models;
    using FinPlusService.Calendar;
    using Microsoft.AspNetCore.Mvc;

    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            ViewBag.UserRole = role;

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddDropRecord([FromBody] DropRecordModel model)
        {
            DateTime selectedDateTime = DateTime.Parse(model.SelectedHour);
            model.Record.Time = selectedDateTime;
            Records newRecord = new Records()
            {
                Day = selectedDateTime,
                DropRecords = model.Record,
                OrganisationId = "000",
            };
            await _calendarService.AddRecord(newRecord);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetDropRecordByDate(DateTime date)
        {
            var records = await _calendarService.GetDropRecordByDate(date);
            return Json(records);
        }
    }
}
