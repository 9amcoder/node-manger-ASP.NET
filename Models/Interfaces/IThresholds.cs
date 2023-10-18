namespace NodeManagementApp.Models
{
    public interface IThresholds
    {
        float MaxUploadUtilization { get; set; }
        float MaxDownloadUtilization { get; set; }
        float MaxErrorRate { get; set; }
        uint MaxConnectedClients { get; set; }
    }
}