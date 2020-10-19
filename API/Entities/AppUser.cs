namespace API.Entities
{
    public class AppUser
    {
        //properties need to be publie to work with Entity framework
        // use Id as for entity framework later it will create an autoincrement
        public int Id { get; set; }
        // case on UserName is crucial for when we implemnet asp Identity done use case = Username it will create lot less refactoring
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}