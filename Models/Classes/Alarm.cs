using NodeManagementApp.Models.Interfaces;

namespace NodeManagementApp.Models.Classes;

public class Alarm : IAlarm
{
    public string NodeId { get; set; }
    public string Metric { get; set; }
    public float Value { get; set; }

    public Alarm()
    {
        NodeId = string.Empty;
        Metric = string.Empty;
        Value = 0.0f;
    }
}