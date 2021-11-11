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
    }
}
