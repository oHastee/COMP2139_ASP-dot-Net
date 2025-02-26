using Microsoft.AspNetCore.Mvc;
using COMP2139_Labs.Data;
using Microsoft.EntityFrameworkCore;

namespace COMP2139_Labs.Areas.ProjectManagement.Components.ProjectSummary
{
    public class ProjectSummaryViewComponent : ViewComponent
    {

        private readonly ApplicationDbContext _context;


        public ProjectSummaryViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IViewComponentResult> InvokeAsync(int projectId)
        {

            var project = await _context.Projects
                .Include(p => p.ProjectTasks)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);



            if (project == null)
            {
                //Handle the case when the project is not found, return Html content
                return Content("Project Not Found");
            }


            return View(project);

        }










    }
}

