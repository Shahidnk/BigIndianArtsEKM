using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
   public class APIResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public string username { get; set; }
        public string role { get; set; }
        public string id { get; set; }
    }
}
