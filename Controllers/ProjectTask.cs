using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_PM.Data;
using Smart_PM.Models;

namespace Smart_PM.Controllers
{
    public class ProjectTasksController : Controller
    {
        private readonly AppDbContext _context;

        public ProjectTasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTasks
        public async Task<IActionResult> Index()
        {
            var tasks = _context.ProjectTasks.Include(t => t.Sprint);
            return View(await tasks.ToListAsync());
        }

        // GET: ProjectTasks/Create
        public IActionResult Create()
        {
            ViewBag.SprintId = new SelectList(_context.Sprints, "SprintId", "Title");
            return View();
        }

        // POST: ProjectTasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask task)
        {
            ModelState.Remove("Sprint");

            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.SprintId = new SelectList(_context.Sprints, "SprintId", "Title", task.SprintId);
            return View(task);
        }

        // GET: ProjectTasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.ProjectTasks.FindAsync(id);
            if (task == null) return NotFound();

            ViewBag.SprintId = new SelectList(_context.Sprints, "SprintId", "Title", task.SprintId);
            return View(task);
        }

        // POST: ProjectTasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProjectTask task)
        {
            if (id != task.ProjectTaskId) return NotFound();

            ModelState.Remove("Sprint");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProjectTasks.Any(e => e.ProjectTaskId == id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.SprintId = new SelectList(_context.Sprints, "SprintId", "Title", task.SprintId);
            return View(task);
        }

        // GET: ProjectTasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var task = await _context.ProjectTasks
                .Include(t => t.Sprint)
                .FirstOrDefaultAsync(m => m.ProjectTaskId == id);

            if (task == null) return NotFound();

            return View(task);
        }

        // POST: ProjectTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.ProjectTasks.FindAsync(id);
            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
