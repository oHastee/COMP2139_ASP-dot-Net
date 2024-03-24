using System;
using System.ComponentModel.DataAnnotations;


namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }


        public Project()
        {
        }

        // Ensure this is a collection indicating a one-to-many relationship
        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
        public ICollection<ProjectComment> ProjectComments { get; set; } = new List<ProjectComment>();
    }

}


