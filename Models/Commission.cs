namespace ConsoleApp1.Models
{
    public class Commission
    {
        public int id { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public Franchise franchise { get; set; }
        public string order_type_display { get; set; }
        public string order_serving_type_display { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string staff_modified_datetime { get; set; }
        public string order_serving_type { get; set; }
        public string order_type { get; set; }
        public bool is_alliance { get; set; }
        public bool is_promotion { get; set; }
        public string name { get; set; }
        public string sub_name { get; set; }
        public decimal commission_rate { get; set; }
        public int commission_amount { get; set; }
        public decimal additional_commission_rate { get; set; }
        public bool is_accept_additional_commission { get; set; }
        public bool is_confirm_vendor_manage_delegation { get; set; }
        public bool is_apply_dynamic_take_rate { get; set; }
        public string sale_by { get; set; }
        public string rank_keyword { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public int payment_plan { get; set; }
    }

    public class CommissionContract
    {
        public int id { get; set; }
        public int vendor { get; set; }
        public string company_number { get; set; }
        public int commission { get; set; }
        public int billing_entity_info { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public int created_by { get; set; }
        public string created_by_slug { get; set; }
        public int modified_by { get; set; }
        public string modified_by_slug { get; set; }
        public string billing_cycle { get; set; }
        public string franchise_type { get; set; }
        public string commission_start_date { get; set; }
        public string commission_end_date { get; set; }
        public string audit_status { get; set; }
        public string get_audit_status_display { get; set; }
        public string audit_modified_datetime { get; set; }
        public int? parent_pk { get; set; }
        public string contract_type { get; set; }
    }

    public class ZeroCommission
    {
        public int id { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string staff_modified_datetime { get; set; }
        public int commission_amount { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
    }

    public class ZeroCommissionContract
    {
        public int id { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public int vendor { get; set; }
        public string company_number { get; set; }
        public ZeroCommission zero_commission { get; set; }
        public AdminArea admin_area { get; set; }
        public Category category { get; set; }
        public string audit_status { get; set; }
        public string get_audit_status_display { get; set; }
        public string audit_modified_datetime { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string staff_modified_datetime { get; set; }
        public string owner_modified_datetime { get; set; }
        public string commission_start_date { get; set; }
        public string commission_end_date { get; set; }
        public bool is_approved { get; set; }
        public int? parent_pk { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public int owner_created_by { get; set; }
        public int owner_modified_by { get; set; }
        public int? principal_company { get; set; }
        public int? next_item { get; set; }
    }

    public class AdminArea
    {
        public string zerocommissioncontract { get; set; }
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public string zerocommissioncontract { get; set; }
        public string vendorcategorycoderelation { get; set; }
        public string vendor { get; set; }
        public string franchisecategorycoderelation { get; set; }
        public int id { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string staff_modified_datetime { get; set; }
        public string category_type { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public int ordering_num { get; set; }
    }

}
