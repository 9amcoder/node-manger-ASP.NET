namespace NodeManagementApp.Models{

    public class Thresholds : IThresholds
    {
        public float MaxUploadUtilization { get; set; }
        public float MaxDownloadUtilization { get; set; }
        public float MaxErrorRate { get; set; }
        public uint MaxConnectedClients { get; set; }
        
    }
}