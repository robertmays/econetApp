using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class EmployeeUpdateDto
    {
        // this dto is only for logged on user
        [Required]
        public string KnownAs { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Initials { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}