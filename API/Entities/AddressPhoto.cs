using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("AddressPhotos")]
    public class AddressPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //fully define the relationship between address and their photo
        // so if you delete an address cascade delete will be implemented
        //in our migration and rule set up in the database
        public Address Address { get; set; }
        public int AddressId { get; set; }
    }
}