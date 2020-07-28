using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Models
{
    public class UserList
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int ListId { get; set; }
        public ItemList List { get; set; }
    }
}
