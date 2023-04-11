using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
   public class GetCommission
    {
        public string id { get; set; }
        public string artist_id { get; set; }
        public string order_id { get; set; }
        public DateTime createdOn { get; set; }
        public string month { get; set; }
        public string date { get; set; }
        public string order_name { get; set; }
        public decimal amount { get; set; }
        public string commission { get; set; }
        public decimal com_amount { get; set; }
        public string status { get; set; }
    }
}
