namespace FinPlus.Domain.Users.Trafer
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Traffer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public FIO? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? ReferalId { get; set; }

        public string? MobileNumber { get; set; }

        public TrafferLevel Level { get; set; }

        public TrafferBet? Bet { get; set; }

        public string? OrganisationId { get; set; }
    }
}
