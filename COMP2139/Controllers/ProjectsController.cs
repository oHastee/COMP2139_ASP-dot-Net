using COMP2139_Labs.Models;
using Microsoft.AspNetCore.Mvc;

namespace Labs.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project { ProjectId = 1, Name = "Project 1", Description = "This is my first project"}
            };

            return View(projects);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Details of project " + id };
            return View(project);
        }

        [HttpPost]
        public IActionResult Create(Project project)
        {
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


    }
}