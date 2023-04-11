using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class DeviceTokensModel
    {
        public int EmployeeId { get; set; }
        public string Platform { get; set; }
        public string CreatedOn { get; set; }
        public string Fcmtoken { get; set; }
        public string DeviceId { get; set; }
    }
}
