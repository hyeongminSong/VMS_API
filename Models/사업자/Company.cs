namespace ConsoleApp1.Models
{
    public class PrincipalCompany
    {
        public string corp_name { get; set; }
        public string name { get; set; }
        public string company_number { get; set; }
        public string company_type { get; set; }
        public string liquor_sales_reg_number { get; set; }
    }
    public class Company
    {
        public int id { get; set; }
        public string name { get; set; }
        public PrincipalCompany principal_company { get; set; }
        public string subordinate_num { get; set; }
    }
}
