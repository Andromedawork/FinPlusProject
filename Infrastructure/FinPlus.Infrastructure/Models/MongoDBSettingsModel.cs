namespace FinPlus.Infrastructure.Models
{
    public class MongoDBSettingsModel
    {
        public string ConnectionURI { get; set; } = null!;

        public string DataBaseName { get; set; } = null!;

        public string DropsCollection { get; set; } = null!;

        public string OffersCollection { get; set; } = null!;

        public string OrganisationsCollection { get; set; } = null!;

        public string AdminsCollection { get; set; } = null!;

        public string RecordsCollection { get; set; } = null!;

        public string TraffersCollection { get; set; } = null!;

        public string DefaultBetsCollection { get; set; } = null!;

        public string SourceCollection { get; set; } = null!;
    }
}
