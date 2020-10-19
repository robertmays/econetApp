using API.Entities;

namespace API.Interfaces
{
    //1 Easy for testing and mocking
    //2 best practice
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}