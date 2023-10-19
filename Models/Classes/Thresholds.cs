using NodeManagementApp.Models.Interfaces;

namespace NodeManagementApp.Models.Classes;

public class Thresholds : IThresholds
{
    public float MaxUploadUtilization { get; set; }
    public float MaxDownloadUtilization { get; set; }
    public float MaxErrorRate { get; set; }
    public uint MaxConnectedClients { get; set; }
}