using System;

namespace ConsoleApp1.Models
{
    public class CommissionContract
    {
        public int vendor { get; set; }
        public int commission { get; set; }
        public int billing_entity_info { get; set; }
        public string franchise_type {  get; set; }
        public string commission_start_date { get; set; }
    }
}
