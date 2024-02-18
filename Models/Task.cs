using System.ComponentModel;

namespace taskmanagementapi.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title  { get; set; }
        public string Description  { get; set; }
        public DateTime DueDate  { get; set; }

        [DefaultValue(true)]
        public bool active { get; set; }
        [DefaultValue(false)]
        public bool status { get; set; }
    }
}
