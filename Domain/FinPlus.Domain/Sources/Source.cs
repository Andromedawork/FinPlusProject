namespace FinPlus.Domain.Sources
{
    using FinPlus.Domain.Users;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Source
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? ReferalId { get; set; }

        public string? OrganizationId { get; set; }
    }
}
