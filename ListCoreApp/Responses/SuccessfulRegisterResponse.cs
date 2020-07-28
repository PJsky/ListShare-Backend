using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ListCoreApp.Responses
{
    public class SuccessfulRegisterResponse
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Message { get; set; } = "Successfully registered a new account";
    }
}
