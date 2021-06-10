using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
             //never trust what a user sends you. validate it from the token first
            //use the claims principle User that we have access to.
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}