public class MongoDbSettings
{
    public MongoDbSettings()
    {
        ConnectionString = string.Empty;
    }

    public string ConnectionString { get; set; }
    public string? DatabaseName { get; set; }
    public string? CollectionName { get; set; }
}