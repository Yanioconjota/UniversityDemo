using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public enum CourseLevel
    {
        Basic,
        Medium,
        Advanced,
        Expert
    }

    public class Course: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;
        
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = String.Empty;
        
        public string? Description { get; set; } = String.Empty;

        [Required]
        public string PublicObjective { get; set; } = String.Empty;

        [Required]
        public string Goals { get; set; } = String.Empty;

        [Required]
        public CourseLevel Level { get; set; } = CourseLevel.Basic;

        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();

        [Required]
        public Chapter Chapter { get; set; } = new Chapter();

        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
