using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_PM.Data;
using Smart_PM.Models;
using System.Linq;

namespace Smart_PM.Controllers
{
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? projectId, int? sprintId, Priority? priority, Status? status)
        {
            ViewBag.Projects = new SelectList(_context.Projects, "ProjectId", "Name");
            ViewBag.Sprints = new SelectList(_context.Sprints, "SprintId", "Title");

            var tasks = _context.ProjectTasks
                .Include(t => t.Sprint)
                .ThenInclude(s => s.Project)
                .AsQueryable();

            if (projectId.HasValue)
            {
                tasks = tasks.Where(t => t.Sprint.ProjectId == projectId.Value);
            }

            if (sprintId.HasValue)
            {
                tasks = tasks.Where(t => t.SprintId == sprintId.Value);
            }

            if (priority.HasValue)
            {
                tasks = tasks.Where(t => t.Priority == priority.Value);
            }

            if (status.HasValue)
            {
                tasks = tasks.Where(t => t.Status == status.Value);
            }

            return View(tasks.ToList());
        }
    }
}
