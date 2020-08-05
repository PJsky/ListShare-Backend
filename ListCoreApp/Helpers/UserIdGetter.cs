using ListCoreApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Helpers
{
    public static class UserIdGetter
    {
        public static int getIdFromToken(Microsoft.AspNetCore.Http.HttpRequest request, DatabaseContext context) {
            var userEmail = TokenBearerValueGetter.getValue(request, "email");
            var userId = context.Users.Where(u => u.Email == userEmail)
                                                 .Select(u => u.Id)
                                                 .First();
            return userId;
        }
    }
}
