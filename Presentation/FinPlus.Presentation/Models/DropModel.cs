namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Users.Drop;

    public class DropModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Source { get; set; }

        public string? PartnerId { get; set; }

        public string? Partner { get; set; }

        public string? SubPartner { get; set; }

        public string? SubPartnerId { get; set; }

        public string? MobileNumber { get; set; }

        public string? Telegram { get; set; }

        public string? Pass { get; set; }

        public string? CardNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Date { get; set; }

        public List<Offer>? Offers { get; set; }

        public Dictionary<DateTime, DropStep>? Step { get; set; }

        public decimal ProfitPotencial { get; set; }

        public decimal RevenuePotential { get; set; }
    }
}