using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using NodeManagementApp.Models;

public class Node : INode
{
    private readonly Random _rnd;

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string NodeId { get; set; }

    [BsonElement("City")]
    public string City { get; set; }
        
    [BsonElement("OnlineTime")]
    public DateTime OnlineTime { get; private set; }

    [BsonElement("IsOnline")]
    public bool IsOnline { get; private set; }
        
    [BsonElement("UploadUtilization")]
    public float UploadUtilization { get; private set; }

    [BsonElement("DownloadUtilization")]
    public float DownloadUtilization  { get; private set; }

    [BsonElement("ErrorRate")]
    public float ErrorRate { get; private set; }

    [BsonElement("ConnectedClients")]
    public uint ConnectedClients { get; private set; }

    public Node()
    {
        _rnd = new Random();
        NodeId = Guid.NewGuid().ToString();
        City = "Default City";
        ResetMetrics();
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
        ConnectedClients = (uint)_rnd.Next(1, 500);
        UploadUtilization = (float)_rnd.NextDouble();
        DownloadUtilization = (float)_rnd.NextDouble();
        ErrorRate = (float)_rnd.NextDouble();
    }
}