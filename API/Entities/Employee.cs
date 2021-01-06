using System;
using System.Collections.Generic;
using API.Extensions;

namespace API.Entities
{
    public class Employee
    {
        //properties need to be public to work with Entity framework
        // use Id as for entity framework later it will create an autoincrement
        public int Id { get; set; }
        // case on UserName is crucial for when we implement asp Identity done use case = Username it will create lot less refactoring
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Lastname { get; set; }
        public ICollection<UserPhoto> Photos {get; set;}

        
        // public int GetAge()
        // {
        //     return DateOfBirth.CalculateAge();;
        // }

    }
}