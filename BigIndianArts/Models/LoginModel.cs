using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class LoginModel
    {
        public string email_id { get; set; }
        public string password { get; set; }
    } 
    public class DeleteOrder
    {
        public int id { get; set; }
    }
    public class UpdateOrderModel
    { 
        public int id { get; set; }
        public string status { get; set; }
        public string image { get; set; }
        public string amount { get; set; }
        public string amountadv { get; set; }
        public string paymentmode { get; set; }
        public DateTime createdOn { get; set; }
        public string title { get; set; }
        public string user_id { get; set; }
        public string user_name { get; set; }
    }
    public class OrderModel
    {
        public int id { get; set; }
        public string order_id { get; set; }
        public string customer_name { get; set; }
        public string bill_number { get; set; }
        public string assign_name { get; set; }
        public int assign_id { get; set; }
        public int assign_empid { get; set; }
        public string orientation { get; set; }
        public string commission { get; set; }
        public string type { get; set; }
        public int people_no { get; set; }
        public string size { get; set; }
        public string frame { get; set; }
        public DateTime expected_date { get; set; }
        public DateTime createdon { get; set; }
        public string contact_number { get; set; }
        public int advance { get; set; }
        public int price { get; set; }
        public string remarks { get; set; }
        public string paymentmode { get; set; }
        public string online_order { get; set; }
        public string isVideo { get; set; }
        public string uploaded_image { get; set; }
        public string status { get; set; }
        public string delivery_mode { get; set; }
    }


}
