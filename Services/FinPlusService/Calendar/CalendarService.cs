namespace FinPlusService.Calendar
{
    using FinPlus.Domain.CalendarOfDrops;

    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository _calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository)
        {
            _calendarRepository = calendarRepository;
        }

        public async Task<List<Records>> GetAllRecords()
        {
            return await _calendarRepository.GetAllRecords();
        }

        public async Task AddRecord(Records record)
        {
            await _calendarRepository.AddRecords(record);
        }

        public async Task<List<DropRecord>> GetDropRecordByDate(DateTime date)
        {
            var records = await _calendarRepository.GetRecordsByDate(date);

            var dropRecords = records
            .SelectMany(r => r.DropRecords?.Where(kv => kv.Key.Date == date.Date).Select(kv => kv.Value))
            .Select(dr => new DropRecord
            {
                ReferalId = dr.ReferalId,
                MobileNumber = dr.MobileNumber,
                Name = dr.Name,
                Surname = dr.Surname,
                Patronymic = dr.Patronymic,
            })
            .ToList();

            return dropRecords;
        }
    }
}
