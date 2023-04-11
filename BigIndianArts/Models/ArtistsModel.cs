using System;
using System.Collections.Generic;
using System.Text;

namespace BigIndianArts.Models
{
    public class ArtistsModel
    {
        public string id { get; set; }
        public string creted_on { get; set; }
        public string dateCreated { get; set; }
        public string name { get; set; }
        public string commission { get; set; }
        public string amount { get; set; }
        public string level { get; set; }
        public string number { get; set; }
        public string remarks { get; set; }
        public string address { get; set; }
        public string profile_image { get; set; }
        public string login_id { get; set; }
        public string emergency_number { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string status { get; set; }


    }
    public class ArtistModel
    {
        public int id { get; set; }
        public string artist_name { get; set; }
        public DateTime createdon { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int commision { get; set; }
        public string level { get; set; }
        public string address { get; set; }
        public string image { get; set; }

        public decimal contact_number { get; set; }
        public decimal emergency { get; set; }

        public string remarks { get; set; }

    }
    public class CandidateModel
    {
        public int Id { get; set; }
        public string name { get; set; }
    }
}
