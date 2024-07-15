namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.CalendarOfDrops;
    using FinPlusService.Calendar;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;

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
        public async Task<ActionResult> AddDropRecord(DropRecord record, DateTime selectedDate)
        {
            Records newRecord = new Records()
            {
                Day = selectedDate,
                DropRecords = new Dictionary<string, DropRecord>
                {
                    { selectedDate.ToString("o"), record },
                },
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
