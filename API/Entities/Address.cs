using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Addresses")]
    public class Address
    {
        public int Id { get; set; }
        public string HouseNumberName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Town { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }

        public ICollection<AddressPhoto> Photos {get; set;}
    }
}