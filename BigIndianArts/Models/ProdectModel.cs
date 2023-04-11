using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class ProductImage
    {
        public string id { get; set; }
        public string product_id { get; set; }
        public string image_Url { get; set; }
    }

    public class ProdectModel
    {
        public string id { get; set; }
        public string cat_id { get; set; }
        public string category_name { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string price { get; set; }
        public string wholesale_price { get; set; }
        public string isoffer { get; set; }
        public string offer_price { get; set; }
        public string product_image { get; set; }
        public string sub_category { get; set; }
        public string stock_count { get; set; }
        public string status { get; set; }
        public List<ProductImage> product_images { get; set; }
    }
   
}
