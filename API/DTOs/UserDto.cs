namespace API.DTOs
{
    public class UserDto
    {
        //this is used for the token only
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
    }
}