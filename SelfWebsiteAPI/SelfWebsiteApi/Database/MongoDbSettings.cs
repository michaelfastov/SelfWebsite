namespace SelfWebsiteApi.Database
{
    public class MongoDbSettings
    {
        public bool IsActive { get; set; }
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string ResumesCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}
