namespace NodeManagementApp.Models
{
    public interface INode
    {
        string NodeId { get; }
        string City { get; }
        DateTime OnlineTime { get; }
        bool IsOnline { get; }
        float UploadUtilization { get; }
        float DownloadUtilization { get; }
        float ErrorRate { get; }
        uint ConnectedClients { get; }
        void SetOnline();
        void SetOffline();
    }
}