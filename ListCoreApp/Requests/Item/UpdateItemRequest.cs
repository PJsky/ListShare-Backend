using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Requests.Item
{
    public class UpdateItemRequest
    {
        public int ItemId { get; set; }
        public bool IsDone { get; set; }
        public string ListAccessCode { get; set; }
        public string ListPassword { get; set; }
    }
}
