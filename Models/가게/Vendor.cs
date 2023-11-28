using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class VendorCategoryUpdate
    {
        public category_set[] category_set { get; set; }
    }
    public class category_set
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category_type { get; set; }
    }
    public class Vendor
    {
        public string name { get; set; }
        public Company company { get; set; }
        public AddressInfo vendor_address { get; set; }
        public string vertical_type {  get; set; }
        public string business_type {  get; set; }
        public string license_number { get; set; }
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
        public termination termination { get; set;}
    }
}
