using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Representative
    {
        public int? id { get; set; }
        public string name { get; set; }
        public bool is_active { get; set; }
        public int principal_company_id { get; set; }
    }
    public class Agent
    {
        public string certi_number { get; set; }
        public string staff_modified_datetime { get; set; }
        public string name { get; set; }
        public string birth_date { get; set; }
        public string certi_datetime { get; set; }
        public string issue_datetime { get; set; }
        public string gender { get; set; }
        public string authority_check_type { get; set; }
        public string nationality { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }
    public class PrincipalCompany
    {
        public string company_number { get; set; }
        public string company_number_code { get; set; }
        public string company_type { get; set; }
        public string name { get; set; }
        public string name_eng { get; set; }
        public Agent agent { get; set; }
        public string business_type { get; set; }
        public string location { get; set; }
        public string taxation_type { get; set; }
        public string corp_number { get; set; }
        public string corp_name { get; set; }
        public string corp_phone { get; set; }
        public string estimated_purpose_file_type { get; set; }
        public string corp_name_eng { get; set; }
        public List<Representative> representative_set { get; set; }
        public string sales_reg_number { get; set; }
        public AddressInfo address { get; set; }
        public string liquor_sales_reg_number { get; set; }
        public string company_status { get; set; }
        public string proprietor_verifi_method { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public string custom_detailed_address { get; set; }
        public string estimated_purpose { get; set; }
    }
    public class Company
    {
        public int id { get; set; }
        public string name { get; set; }
        public PrincipalCompany principal_company { get; set; }
        public string subordinate_num { get; set; }
    }
}
