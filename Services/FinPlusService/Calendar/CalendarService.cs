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

            List<DropRecord> dropRecords = new List<DropRecord>();

            foreach (var record in records)
            {
                if (record.DropRecords != null)
                {
                    dropRecords.Add(record.DropRecords);
                }
            }

            return dropRecords;
        }
    }
}
