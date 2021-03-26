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

        /// <summary>
        /// Get user's database Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string GetToken(this ClaimsPrincipal user)
        {
            return Get(user, "DbToken");
        }        
    }
}
