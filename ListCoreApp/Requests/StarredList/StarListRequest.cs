using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Requests.StarredList
{
    public class StarListRequest
    {
        public int UserId { get; set; }
        public int ListId { get; set; }
        public string AccessCode { get; set; }
    }
}
