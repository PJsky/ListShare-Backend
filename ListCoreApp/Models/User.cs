using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public byte[] SecurityHash { get; set; }

        public ICollection<UserList> UserLists { get; set; }
    }
}
