using System;

namespace ConsoleApp1.Models
{
    public class Franchise
    {
        public string commission { get; set; }
        public string franchisestaffrelation { get; set; }
        public string staff { get; set; }
        public string vendor { get; set; }
        public string franchise { get; set; }
        public string franchiseextra { get; set; }
        public string franchisecontract { get; set; }
        public string franchiseticket { get; set; }
        public string franchisenoticefranchiserelation { get; set; }
        public string franchisenotice { get; set; }
        public string user { get; set; }
        public int id { get; set; }
        public bool is_active { get; set; }
        public DateTime created_datetime { get; set; }
        public DateTime modified_datetime { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public DateTime staff_modified_datetime { get; set; }
        public string name { get; set; }
        public string principal_company { get; set; }
        public string group { get; set; }
        public bool is_group { get; set; }
    }
}
