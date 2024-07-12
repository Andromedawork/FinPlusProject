namespace FinPlus.Presentation.Controllers
{
    using FinPlus.Domain.CalendarOfDrops;
    using FinPlusService.Calendar;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Driver;

    [Route("api/[controller]")]
    [ApiController]
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

        public async Task<ActionResult> AddDropRecord(DropRecord record)
        {
            Records newRecord = new Records()
            {
                Day = DateTime.Now,
                DropRecords = new Dictionary<DateTime, DropRecord>
                {
                    { DateTime.Now, record },
                },
                CreatedAt = DateTime.Now,
                OrganisationId = "000",
            };
            await _calendarService.AddRecord(newRecord);
            return RedirectToAction("Index");
        }

        [HttpGet("GetRecordsByDate")]
        public async Task<IActionResult> GetRecordsByDate(DateTime date)
        {
            try
            {
                var dropRecords = await _calendarService.GetDropRecordByDate(date);
                return Ok(dropRecords);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ошибка сервера: {ex.Message}");
            }
        }
    }
}
