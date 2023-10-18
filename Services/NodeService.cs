using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using NodeManagementApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System.Threading.Tasks;

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
        }

        public async Task<Node> CreateAsync(Node node)
        {
            await _nodes.InsertOneAsync(node);
            return node;
        }

        public async Task<IList<Node>> ReadAsync() => 
            await _nodes.AsQueryable().ToListAsync();

        public async Task<Node> FindAsync(string nodeId) => 
            await _nodes.Find(node => node.NodeId == nodeId).SingleOrDefaultAsync();

        public async Task UpdateAsync(string nodeId, Node node) =>
             await _nodes.ReplaceOneAsync(n => n.NodeId == nodeId, node);

        public async Task DeleteAsync(string nodeId) => 
            await _nodes.DeleteOneAsync(node => node.NodeId == nodeId);

        public async Task<ITelemetry> GetTelemetryAsync(string nodeId)
        {
            var node = await _nodes.Find(n => n.NodeId == nodeId).SingleOrDefaultAsync();
            return node.Telemetry;
        }

        public async Task UpdateThresholdsAsync(string nodeId, IThresholds thresholds)
        {
            var node = await _nodes.Find(n => n.NodeId == nodeId).SingleOrDefaultAsync();
            node.Thresholds = (Thresholds)thresholds;
            await _nodes.ReplaceOneAsync(n => n.NodeId == nodeId, node);
        }

        public async Task<IEnumerable<IAlarm>> GetAlarmsAsync()
        {
            var nodes = await _nodes.AsQueryable().ToListAsync();
            var alarms = nodes.SelectMany(n => n.Alarms);
            return alarms;
        }
    }
}