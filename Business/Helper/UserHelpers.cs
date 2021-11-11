using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Business.Helper
{
    public class UserHelpers
    {
        public static string GetUserIdByClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == "nameid").Select(c => c.Value).SingleOrDefault();
        }

        public static string GetUserEmailByClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault();
        }

        public static string GetUserNameByClaim(IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
        }

        public static IEnumerable<string> GetUserRolesByClaim(IEnumerable<Claim> claims)
        {
            yield return claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
        }
    }
}
