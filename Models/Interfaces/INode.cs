using NodeManagementApp.Models.Classes;

namespace NodeManagementApp.Models.Interfaces;

public interface INode
{
    string NodeId { get; set; }
    string City { get; set; }
    DateTime OnlineTime { get; set; }
    bool IsOnline { get; set; }
    float UploadUtilization { get; set; }
    float DownloadUtilization { get; set; }
    float ErrorRate { get; set; }
    uint ConnectedClients { get; set; }
    Thresholds Thresholds { get; set; }
    List<IAlarm> Alarms { get; set; }
}