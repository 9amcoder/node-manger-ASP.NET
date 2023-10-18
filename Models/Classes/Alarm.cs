namespace NodeManagementApp.Models
{
    public class Alarm : IAlarm
    {
        public string NodeId { get; set; }
        public string Metric { get; set; }
        public float Value { get; set; }

        public Alarm()
        {
            NodeId = Guid.NewGuid().ToString();
            Metric = "Default Metric";
            Value = 0;
        }

    }
}