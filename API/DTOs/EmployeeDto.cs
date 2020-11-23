using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class EmployeeDto
    {
        
        public int Id { get; set; }
        // case on UserName is crucial for when we implemnet asp Identity done use case = Username it will create lot less refactoring
        public string Username { get; set; } 
        public string PhotoUrl { get; set; }      
        public string KnownAs { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
         public int Age { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Lastname { get; set; }
        public ICollection<UserPhotoDto> Photos {get; set;}
    }
}