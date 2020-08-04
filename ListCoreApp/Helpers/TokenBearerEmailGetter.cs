using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace ListCoreApp.Helpers
{
    public static class TokenBearerValueGetter
    {
        public static string getValue(Microsoft.AspNetCore.Http.HttpRequest request, string value) {
            var stream = request.Headers["Authorization"].ToString().Remove(0, 7);
            var handler = new JwtSecurityTokenHandler();
            var tokenData = handler.ReadToken(stream) as JwtSecurityToken;
            try
            {
                var tokenBearerValue = tokenData.Payload.Where(v => v.Key == value).First().Value;
                return tokenBearerValue.ToString();
            }
            catch {
                return "Not Found";
            }
        }
    }
}
