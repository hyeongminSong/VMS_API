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
    }

    public class VendorBasicInfo
    {
        public string name { get; set; }
        public string company { get; set; }
    }
}
