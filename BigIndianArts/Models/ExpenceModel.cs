using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class ExpenceModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public string paymentmode { get; set; }
        public string amount { get; set; }
        public string remarks { get; set; }
        public DateTime createdOn { get; set; }
    }
    public class GetExpenceModel
    {
        public string id { get; set; }
        public string Mode { get; set; }
        public string type { get; set; }
        public string expence_date { get; set; }
        public string paymentmode { get; set; }
        public string month { get; set; }
        public string year { get; set; }
        public DateTime createdDate { get; set; }
        public string amount { get; set; }
        public string remarks { get; set; }
    }


    public class GetExpenceIncomeModel
    {
        public string Inid { get; set; }
        public string Exid { get; set; }
        public string exptype { get; set; }
        public string intype { get; set; }
        public string paymentmode { get; set; }
        public string expence_date { get; set; }
        public string income_date { get; set; }
        public DateTime createdDate { get; set; }
        public string expense_amount { get; set; }
        public string income_amount { get; set; }
        public string remarks { get; set; }
    }
    public class DailyReportModel
    {
        public string id { get; set; }
       
        public string type { get; set; }
        public string title { get; set; }
        public string paymentmode { get; set; }
        public string icon { get; set; }
       
        public string date { get; set; }
       
        public string amount { get; set; }
       
        public string remarks { get; set; }
    }
}
