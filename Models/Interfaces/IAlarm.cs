namespace NodeManagementApp.Models.Interfaces;

public interface IAlarm
{
    string NodeId { get; set; }
    string Metric { get; set; }
    float Value { get; set; }
}