using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("UserPhotos")]
    public class UserPhoto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        //fully define the relationship between user and their photo
        // so if you delete a user cascade delete will be implemented
        //in our migration and rule set up in the database
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}