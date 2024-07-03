namespace FinPlus.Domain.Users.Drop
{
    using FinPlus.Domain.Offers;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Drop
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public FIO? Name { get; set; }

        public string? ReferalId { get; set; }

        public DropPassport? Pass { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>(0);

        public bool MoneyStatus { get; set; }

        public DropCategory DropCategory { get; set; }

        public int Point { get; set; }

        public decimal RevenuePotential { get; set; }

        public decimal ProfitPotencial { get; set; }

        public decimal TrafferProfitPotencial { get; set; }

        public decimal Payment { get; set; }

        public string? MobileNumber { get; set; }

        public string? CardNumber { get; set; }

        public string? PersonalReferalId { get; set; }

        public Dictionary<DateTime, DropStep>? Steps { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateOfBirth { get; set; }

        public string? OrganisationId { get; set; }

        public string? PaymentTransactionNumber { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime DateEnd { get; set; }

        public Dictionary<DropStep, string>? Comments { get; set; }
    }
}