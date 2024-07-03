namespace FinPlus.Domain.Organisations
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Organisation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? MainAdminId { get; set; }
    }
}
