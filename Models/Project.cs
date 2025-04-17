using System.ComponentModel.DataAnnotations;

namespace Smart_PM.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Sprint> Sprints { get; set; } = new List<Sprint>();
    }
}