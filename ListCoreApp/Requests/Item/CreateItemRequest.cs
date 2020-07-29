using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Requests.Item
{
    public class CreateItemRequest
    {
        public string Name { get; set; } = "No name";
        public string ListAccessCode { get; set; }
        public string ListPassword { get; set; }
    }
}
