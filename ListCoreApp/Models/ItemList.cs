using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Models
{
    public class ItemList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public List<Item> Items { get; set; }
        public ICollection<UserList> UserLists { get; set; }

    }
}
