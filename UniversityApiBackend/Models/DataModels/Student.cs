using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class Student: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime Dob { get; set; }

        [Required]
        public ICollection<Course> Courses { get; set; } = new List<Course>();

        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - Dob.Year;
                if (Dob.Date > today.AddYears(-age)) age--;
                return age;
            }
        }
    }
}
