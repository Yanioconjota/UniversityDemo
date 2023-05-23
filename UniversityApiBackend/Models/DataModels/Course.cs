using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum CourseLevel
    {
        Basic,
        Intermediate,
        Advanced
    }

    public class Course: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;
        
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = String.Empty;
        
        public string? LongDescription { get; set; } = String.Empty;

        [Required]
        public string PublicObjective { get; set; } = String.Empty;

        [Required]
        public string Goals { get; set; } = String.Empty;
        
        [Required]
        public CourseLevel Level { get; set; }
    }
}
