namespace FinPlus.Domain.Users.Admin
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Admin
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public Role Role { get; set; }

        public string? Name { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? MobileNumber { get; set; }

        public string? OrganisationId { get; set; }
    }
}