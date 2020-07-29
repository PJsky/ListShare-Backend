using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Requests.Item
{
    public class ItemDeleteRequest
    {
        public int ItemId { get; set; }
        public string ListAccessCode { get; set; }
        public string ListPassword { get; set; }
    }
}
