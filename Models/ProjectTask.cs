using System.ComponentModel.DataAnnotations;

namespace Smart_PM.Models
{
    public enum Priority { Low, Medium, High }
    public enum Status { InProgress, Completed }

    public class ProjectTask
    {
        public int ProjectTaskId { get; set; }

        [Required]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public string? Tag { get; set; }

        // Relação com Sprint
        public int SprintId { get; set; }
        public Sprint Sprint { get; set; }
    }
}
