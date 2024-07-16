namespace FinPlusService.Calendar
{
    using FinPlus.Domain.CalendarOfDrops;

    public interface ICalendarService
    {
        public Task<List<Records>> GetAllRecords();

        public Task AddRecord(Records record);

        public Task<List<DropRecord>> GetDropRecordByDate(DateTime date);

        public Task DeleteRecord(string id);
    }
}
