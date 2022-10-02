using System.Security.Claims;

namespace RunningGroupsWeb
{
    public static class ClaimsPricipleExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
