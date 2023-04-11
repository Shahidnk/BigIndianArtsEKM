using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class IncomeModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public string order_id { get; set; }
        public string user_id { get; set; }
        public string amount { get; set; }
        public string paymentmode { get; set; }
        public string date { get; set; }
        public string month { get; set; }
        public string year { get; set; }
    }
    
}
