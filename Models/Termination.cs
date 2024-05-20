using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class TerminationReasonSet
    {
        public int id { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public string reason { get; set; }
        public bool is_active { get; set; }
    }
    public class termination
    {
        public int reason { get; set; }
        public string termination_date { get; set; }

    }

    public class BulkCreateTerminationVendor
    {
        public int company_id { get; set; }
        public string company_number { get; set; }
        public int[] vendor_ids { get; set; }
        public termination termination { get; set; }
    }
}
