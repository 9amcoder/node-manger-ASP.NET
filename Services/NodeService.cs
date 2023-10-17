using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using NodeManagementApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace NodeManagementApp.Services
{
    public class NodesService
    {
        private readonly IMongoCollection<Node> _nodes;

        public NodesService(IOptions<MongoDbSettings> settings)
        {
            var clientSettings = MongoClientSettings.FromConnectionString(settings.Value.ConnectionString);
            clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(clientSettings);
            
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _nodes = database.GetCollection<Node>(settings.Value.CollectionName);

            // Ping the database
            try {
                var command = new BsonDocument("ping", 1);
                var commandResponse = database.RunCommand<BsonDocument>(command);
                Console.WriteLine("Connected to MongoDB!");
            }
            catch (Exception ex) {
                Console.WriteLine("Could not connect to MongoDB: ", ex.Message);
            }
        }

        public Node Create(Node node)
        {
            _nodes.InsertOne(node);
            return node;
        }

        public IList<Node> Read() => 
            _nodes.AsQueryable().ToList();

        public Node Find(string nodeId) => 
            _nodes.Find(node => node.NodeId == nodeId).SingleOrDefault();

        public void Update(string nodeId, Node node) =>
             _nodes.ReplaceOne(n => n.NodeId == nodeId, node);

        public void Delete(string nodeId) => 
            _nodes.DeleteOne(node => node.NodeId == nodeId);
    }
}