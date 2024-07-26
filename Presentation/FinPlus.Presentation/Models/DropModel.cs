namespace FinPlus.Presentation.Models
{
    using FinPlus.Domain.Offers;
    using FinPlus.Domain.Sources;
    using FinPlus.Domain.Users.Drop;
    using FinPlus.Domain.Users.Trafer;

    public class DropModel
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Source { get; set; }

        public string? Partner { get; set; }

        public string? SubPartner { get; set; }

        public string? MobileNumber { get; set; }

        public string? PersonalReferalId { get; set; }

        public string? Telegram { get; set; }

        public string? Pass { get; set; }

        public string? CardNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime Date { get; set; }

        public List<string>? Offers { get; set; }

        public Dictionary<DateTime, DropStep>? Step { get; set; }

        public decimal ProfitPotencial { get; set; }

        public decimal RevenuePotential { get; set; }

        public List<Traffer>? AllTraffers { get; set; }

        public List<Source>? AllSources { get; set; }

        public List<Offer>? AllOffers { get; set; }

        public Dictionary<string, List<Offer>?> ReceivedOffers { get; set; }

        public List<string>? Comment { get; set; }
    }
}