﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListCoreApp.Responses
{
    public class SuccessfulListCreation
    {
        public string Name { get; set; }
        public string AccessCode { get; set; }
        public string Message { get; set; } = "List created successfully";
    }
}
