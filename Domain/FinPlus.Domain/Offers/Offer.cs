namespace FinPlus.Domain.Offers
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Offer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public OfferType Type { get; set; }

        public string? Name { get; set; }

        public decimal Profit { get; set; }

        public decimal TargetAction { get; set; }

        public string? OrganisationId { get; set; }

        public Dictionary<string, OfferStatus> Status { get; set; }
    }
}
