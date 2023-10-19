namespace NodeManagementApp.Models.Interfaces;

public interface ITelemetry
{
    float UploadUtilization { get; set; }
    float DownloadUtilization { get; set; }
    float ErrorRate { get; set; }
    uint ConnectedClients { get; set; }
}
