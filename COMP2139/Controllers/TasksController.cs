using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Data;
using COMP2139_Labs.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lists tasks for a specific project
        [HttpGet]
        public IActionResult Index(int projectId)
        {
            var tasks = _context.ProjectTasks.Where(t => t.ProjectId == projectId).ToList();
            ViewBag.ProjectId = projectId; // Pass projectId back to the view for use in Create link
            return View(tasks);
        }




        // Shows the form to create a new task for a specific project
        [HttpGet]
        public IActionResult Create(int projectId)
        {
            // Initialize a new ProjectTask with the ProjectId to ensure it's correctly passed to the form
            var projectTask = new ProjectTask { ProjectId = projectId };
            return View(projectTask);
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                // Manually set ProjectId if not binding correctly
                projectTask.ProjectId = int.Parse(Request.Form["ProjectId"]);

                _context.ProjectTasks.Add(projectTask);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { projectId = projectTask.ProjectId });
            }
            return View(projectTask);
        }




        // Shows details of a specific task
        [HttpGet]
        public IActionResult Details(int id)
        {
            var task = _context.ProjectTasks.FirstOrDefault(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }




        // Shows the form to edit an existing task
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = _context.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }




        // Processes the updating of a task
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ProjectTaskId", "Title", "Description", "ProjectId")] ProjectTask projectTask)
        {
            if (id != projectTask.ProjectTaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.ProjectTasks.Update(projectTask);
                _context.SaveChanges();
                return RedirectToAction("Index", new { projectId = projectTask.ProjectId });
            }
            ViewBag.projects = new SelectList(_context.Projects, "ProjectId", "Name", projectTask.ProjectId);
            return View(projectTask);

        }




        // Confirms the deletion of a task
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var task = _context.ProjectTasks.Include(t => t.Project).FirstOrDefault(t => t.ProjectTaskId == id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.projects = new SelectList(_context.Projects, "ProjectId", "Name", task.ProjectId);
            return View(task);
        }




        // Processes the deletion of a task
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int projectTaskId)
        {
            var task = _context.ProjectTasks.Find(projectTaskId);

            if (task != null)
            {
                _context.ProjectTasks.Remove(task);
                _context.SaveChanges();
                return RedirectToAction("Index", new { projectId = task.ProjectId });
            }
            return NotFound();
        }
    }
}
