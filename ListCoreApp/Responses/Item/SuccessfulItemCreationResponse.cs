using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Responses.Item
{
    public class SuccessfulItemCreationResponse
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ListName { get; set; }
    }
}
