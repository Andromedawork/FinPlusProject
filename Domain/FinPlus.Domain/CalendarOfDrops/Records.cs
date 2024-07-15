namespace FinPlus.Domain.CalendarOfDrops
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using MongoDB.Bson.Serialization.Options;

    public class Records
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Day { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.Document)]
        public Dictionary<string, DropRecord>? DropRecords { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? OrganisationId { get; set; }
    }
}
