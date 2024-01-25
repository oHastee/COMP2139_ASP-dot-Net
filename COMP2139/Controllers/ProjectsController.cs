using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Controllers;
using COMP2139_Labs.Models;

namespace COMP2139_Labs.Controllers
{
    public class ProjectsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var projects = new List<Project>()
            {
                new Project {ProjectId = 1, Name = "Project 1", Description = "My First Project"} //we define a project
            };

            return View(projects); //we pass the project to the view
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Project project)
        {
            return RedirectToAction("Index");

        }






        [HttpGet]
        public IActionResult Details(int id)
        {
            var project = new Project { ProjectId = id, Name = "Project " + id, Description = "Details of the project " + id };
            return View(project);
        }
    }
}