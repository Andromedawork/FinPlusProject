namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Users.Drop;

    public class SourceModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? ReferalId { get; set; }

        public List<Drop>? MyDrops { get; set; }

        public decimal Revenue { get; set; }

        public decimal ProfitInMonth { get; set; }
    }
}
