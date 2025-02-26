using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2139_Labs.Areas.ProjectManagement.Models
{
    public class ProjectTask
    {
        [Key]
        public int ProjectTaskId { get; set; } // Primary key

        [Required(ErrorMessage = "Title is required.")] // Make error messages more descriptive
        [StringLength(100, ErrorMessage = "Title must not exceed 100 characters.")]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; } // Made nullable to emphasize optionality

        [Required(ErrorMessage = "Project is required.")] // Ensures a task is linked to a project
        [Display(Name = "Project")]
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; } // Navigation property to the Project
    }
}
