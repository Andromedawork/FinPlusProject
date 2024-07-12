namespace FinPlus.Domain.CalendarOfDrops
{
    public interface ICalendarRepository
    {
        public Task<List<Records>> GetAllRecords();

        public Task AddRecords(Records records);

        public Task<List<Records>> GetRecordsByDate(DateTime date);
    }
}
