using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Requests
{
    public class GetListRequest
    {
        public string AccessCode { get; set; }
        public string ListPassword { get; set; }
    }
}
