﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Smart_PM.Models
{
    public class Sprint
    {
        public int SprintId { get; set; }

        [Required]
        public string Title { get; set; }

        // Relação com Project
        public int ProjectId { get; set; }
        [ValidateNever]
        public Project Project { get; set; }
        [ValidateNever]
        public List<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
    }
}