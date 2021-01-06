namespace API.DTOs
{
    public class EmployeeUpdateDto
    {
        public int Id {get; set;}
        public string KnownAs { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Lastname { get; set; }
    }
}