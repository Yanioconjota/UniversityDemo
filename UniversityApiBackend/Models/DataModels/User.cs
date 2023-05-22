using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{
    public class User: BaseEntity
    {
        [Required, StringLength(50)]
        public string Name { get; set; } = String.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = String.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        public string Password { get; set; } = String.Empty;
    }
}
