using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NodeManagementApp.Models.Interfaces;

namespace NodeManagementApp.Models.Classes;

public class Node : INode
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string NodeId { get; set; }

    [BsonElement("City")]
    public string City { get; set; }
        
    [BsonElement("OnlineTime")]
    public DateTime OnlineTime { get; set; }

    [BsonElement("IsOnline")]
    public bool IsOnline { get; set; }
        
    [BsonElement("UploadUtilization")]
    public float UploadUtilization { get; set; }

    [BsonElement("DownloadUtilization")]
    public float DownloadUtilization { get; set; }

    [BsonElement("ErrorRate")]
    public float ErrorRate { get; set; }

    [BsonElement("ConnectedClients")]
    public uint ConnectedClients { get; set; }

    public Thresholds Thresholds { get; set; }

    private List<Alarm> _alarms;
    public List<IAlarm> Alarms 
    { 
        get { return _alarms.Cast<IAlarm>().ToList(); }
        set { _alarms = value.Cast<Alarm>().ToList(); }
    }
    
    public Telemetry Telemetry 
    { 
        get 
        {
            return new Telemetry
            {
                UploadUtilization = this.UploadUtilization,
                DownloadUtilization = this.DownloadUtilization,
                ErrorRate = this.ErrorRate,
                ConnectedClients = this.ConnectedClients
            };
        }

        private set { }
    }

    public Node()
    {
        NodeId = Guid.NewGuid().ToString();
        City = "Default City";
        ResetMetrics();
        Thresholds = new Thresholds();
        _alarms = new List<Alarm>();
    }

    public void SetOnline()
    {
        IsOnline = true;
        SimulateRandomMetrics();
    }

    public void SetOffline()
    {
        IsOnline = false;
        ResetMetrics();
    }

    private void ResetMetrics()
    {
        ConnectedClients = 0;
        UploadUtilization = 0.0f;
        DownloadUtilization = 0.0f;
        ErrorRate = 0.0f;
    }

    private void SimulateRandomMetrics()
    {
        var rnd = new Random();
        ConnectedClients = (uint)rnd.Next(1, 500);
        UploadUtilization = (float)rnd.NextDouble();
        DownloadUtilization = (float)rnd.NextDouble();
        ErrorRate = (float)rnd.NextDouble();
    }
}