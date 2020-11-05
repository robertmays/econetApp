using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Extensions;

namespace API.Entities
{
    [Table("Customers")]
    public class Customer
    {
        public int Id { get; set; }
        public string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
         public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Lastname { get; set; }
        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
        //fully define the relationship between customer and address
         public Address Address { get; set; }
         public int AddressId { get; set; }
    }
}