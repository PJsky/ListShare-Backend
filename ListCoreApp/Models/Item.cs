using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int ListId { get; set; }
        public ItemList ItemList { get; set; }
        public string Name { get; set; }
        public bool IsDone { get; set; }
    }
}
