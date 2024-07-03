namespace FinPlus.Domain.Organisations
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class DefaultBet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public Dictionary<int, double>? Bet { get; set; }

        public string? OrganisationId { get; set; }
    }
}
