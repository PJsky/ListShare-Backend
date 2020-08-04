using ListCoreApp.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Helpers
{
    public static class TokenGenerator
    {

        public static string GetToken(IConfiguration config, string email)
        {
            var jwt = new JwtService(config);
            var token = jwt.GenerateSecurityToken(email);
            return token;
        }
    }
}
