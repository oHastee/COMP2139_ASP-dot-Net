using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using COMP2139_Labs.Data;
using COMP2139_Labs.Areas.ProjectManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Controllers
{
    [Area("ProjectManagement")]
    [Route("[area]/[controller]/[action]")]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Index/{projectId:int?}")]
        public async Task<IActionResult> Index(int? projectId)
        {
            var tasksQuery = _context.ProjectTasks.Include(t => t.Project).AsQueryable();

            if (projectId.HasValue)
            {
                tasksQuery = tasksQuery.Where(t => t.ProjectId == projectId.Value);
            }

            var tasks = await tasksQuery.ToListAsync();
            ViewBag.ProjectId = projectId;
            return View(tasks);
        }

        [HttpGet("Create/{projectId:int}")]
        public IActionResult Create(int projectId)
        {
            var taskViewModel = new ProjectTask { ProjectId = projectId };
            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name", projectId);
            return View(taskViewModel);
        }

        [HttpPost("Create/{projectId:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,ProjectId")] ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { projectId = projectTask.ProjectId });
            }

            // Repopulate SelectList in case of validation error
            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var projectTask = await _context.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null) return NotFound();
            return View(projectTask);
        }

        [HttpGet("Edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            if (projectTask == null) return NotFound();
            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        [HttpPost("Edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectTaskId,Title,Description,ProjectId")] ProjectTask projectTask)
        {
            if (id != projectTask.ProjectTaskId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectTaskExists(projectTask.ProjectTaskId)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index), new { projectId = projectTask.ProjectId });
            }
            // Repopulate SelectList in case of validation error
            ViewBag.ProjectId = new SelectList(_context.Projects, "ProjectId", "Name", projectTask.ProjectId);
            return View(projectTask);
        }

        [HttpGet("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var projectTask = await _context.ProjectTasks.Include(t => t.Project).FirstOrDefaultAsync(m => m.ProjectTaskId == id);
            if (projectTask == null) return NotFound();
            return View(projectTask);
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectTask = await _context.ProjectTasks.FindAsync(id);
            _context.ProjectTasks.Remove(projectTask);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { projectId = projectTask.ProjectId });
        }

        private bool ProjectTaskExists(int id)
        {
            return _context.ProjectTasks.Any(e => e.ProjectTaskId == id);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search(int? projectId, string searchString)
        {
            var taskQuery = _context.ProjectTasks.Include(t => t.Project).AsQueryable();

            if (projectId.HasValue)
            {
                taskQuery = taskQuery.Where(t => t.ProjectId == projectId.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                taskQuery = taskQuery.Where(t => t.Title.Contains(searchString) || t.Description.Contains(searchString));
            }

            var tasks = await taskQuery.ToListAsync();
            ViewBag.ProjectId = projectId;
            return View("Index", tasks);
        }
    }
}
