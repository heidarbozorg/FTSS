using System.Security.Claims;

namespace FTSS.API.Extensions
{
    public static class User
    {
        private static string Get(ClaimsPrincipal user, string key)
        {
            if (user == null)
                return null;

            var item = user.FindFirst(key);
            if (item == null)
                return null;

            return item.Value;

        }

        public static string GetToken(this ClaimsPrincipal user)
        {
            return Get(user, "Token");
        }        
    }
}
