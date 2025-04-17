using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart_PM.Data;
using Smart_PM.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Smart_PM.Controllers
{
    public class SprintsController : Controller
    {
        private readonly AppDbContext _context;

        public SprintsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sprints
        public async Task<IActionResult> Index()
        {
            var sprints = await _context.Sprints
                .Include(s => s.Project) // importante para exibir o nome do projeto
                .ToListAsync();

            return View(sprints);
        }


        // GET: Sprints/Create
        public IActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sprint sprint)
        {
            ModelState.Remove("Project");
            ModelState.Remove("Tasks");

            if (ModelState.IsValid)
            {
                _context.Sprints.Add(sprint);
                await _context.SaveChangesAsync();
                Console.WriteLine("✅ Sprint saved!");
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name", sprint.ProjectId); // necessário para repopular dropdown
            Console.WriteLine("❌ ModelState invalid");
            return View(sprint);
        }



        // GET: Sprints/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint == null) return NotFound();

            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", sprint.ProjectId);
            return View(sprint);
        }

        // POST: Sprints/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sprint sprint)
        {
            if (id != sprint.SprintId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sprint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Sprints.Any(e => e.SprintId == id)) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ProjectId"] = new SelectList(_context.Projects, "ProjectId", "Name", sprint.ProjectId);
            return View(sprint);
        }

        // GET: Sprints/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sprint = await _context.Sprints
                .Include(s => s.Project)
                .FirstOrDefaultAsync(m => m.SprintId == id);

            if (sprint == null) return NotFound();

            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sprint = await _context.Sprints.FindAsync(id);
            if (sprint != null)
            {
                _context.Sprints.Remove(sprint);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
