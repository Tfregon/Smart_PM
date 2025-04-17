using Smart_PM.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Smart_PM.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
    }
}