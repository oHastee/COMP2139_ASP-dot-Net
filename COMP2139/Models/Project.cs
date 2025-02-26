using System;
using System.ComponentModel.DataAnnotations;

namespace COMP2139_Labs.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "The Name field is required.")]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }

        // Ensure this is a collection indicating a one-to-many relationship
        public ICollection<ProjectTask> ProjectTasks { get; set; } = new List<ProjectTask>();
    }

}


