using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BigIndianArts.Models
{
    public class GetOrderModel
    {
        public string id { get; set; }
        public string order_id { get; set; }
        public string user_id { get; set; }
        public string createdOn { get; set; }
        public string customer_name { get; set; }
        public string assign_id { get; set; }
        public string assign_empid { get; set; }
        public string assign_name { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string people_no { get; set; }
        public string size { get; set; }
        public string frame { get; set; }
        public string paymentmode { get; set; }
        public string final_image { get; set; }
        public string expected_date { get; set; }
        public string icon { get; set; }
        public string contact_number { get; set; }
        public string commission { get; set; }
        public string uploaded_image { get; set; }
        public string advance { get; set; }
        public string price { get; set; }
        public string balance { get; set; }
        public string remarks { get; set; }
        public string orientation { get; set; }
        public string status { get; set; }
        public Color BgColour { get; set; }
        public int notificationStatus { get; set; }
        public int artist_read { get; set; }
        public int admin_read { get; set; }
        public string request_status { get; set; }
        public string online_order { get; set; }
        public string isVideo { get; set; }
        public string bill_number { get; set; }
        public string delivery_mode { get; set; }
    }

    public class UpdateAction
    {
        public int id { get; set; }
        public DateTime createdOn { get; set; }
        public string title { get; set; }
        public string status { get; set; }
        public string action { get; set; }
        public string user_id { get; set; }
        public string assign_empid { get; set; }
    }

}
