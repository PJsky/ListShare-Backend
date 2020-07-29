using ListCoreApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Responses.ItemList
{
    public class SuccessfulGetListResponse
    {
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public bool IsPublic { get; set; }
        public IEnumerable<Object> Items { get; set; }

    }
}
