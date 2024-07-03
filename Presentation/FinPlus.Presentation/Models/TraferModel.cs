namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Users;
    using FinPlus.Domain.Users.Drop;
    using FinPlus.Domain.Users.Trafer;

    public class TraferModel
    {
        public string? Id { get; set; }

        public FIO? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? ReferalId { get; set; }

        public string? MobileNumber { get; set; }

        public TrafferLevel Level { get; set; }

        public TrafferBet? Bet { get; set; }

        public List<Drop> MyDrops { get; set; } = new List<Drop>();

        public decimal Revenue { get; set; }

        public decimal ProfitInMonth { get; set; }
    }
}
