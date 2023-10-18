using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NodeManagementApp.Models{

    public class Telemetry : ITelemetry
    {
        [BsonElement("UploadUtilization")]
        public float UploadUtilization { get; set; }

        [BsonElement("DownloadUtilization")]
        public float DownloadUtilization { get; set; }

        [BsonElement("ErrorRate")]
        public float ErrorRate { get; set; }

        [BsonElement("ConnectedClients")]
        public uint ConnectedClients { get; set; }

        public Telemetry()
        {
            // Parameterless constructor
        }
    }
}