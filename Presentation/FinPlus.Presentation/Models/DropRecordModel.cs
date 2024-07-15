namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.CalendarOfDrops;

    public class DropRecordModel
    {
        public DropRecord Record { get; set; }

        public DateTime SelectedDate { get; set; }

        public string SelectedHour { get; set; }
    }
}
