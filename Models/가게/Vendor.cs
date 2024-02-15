using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class category_set
    {
        public int id { get; set; }
        public string name { get; set; }
        public string category_type { get; set; }
    }
    public class BillingEntityInfo
    {
        public string bank_account { get; set; }
        public bool is_active { get; set; }
        public string staff_modified_datetime { get; set; }
        public string bill_recv_email { get; set; }
        public string bank_code { get; set; }
        public string bank_account_owner_name { get; set; }
        public string billing_entity_type { get; set; }
        public string default_billing_cycle { get; set; }
        public string salesforce_id { get; set; }
        public string salesforce_address { get; set; }
        public string shipping_address { get; set; }
        public string shipping_address_detail { get; set; }
        public bool is_subscribing_invoice { get; set; }
        public string memo { get; set; }
        public int created_by { get; set; }
        public int modified_by { get; set; }
        public int? bill_recv_address { get; set; }
    }
    public class Vendor
    {
        public string name { get; set; }
        public Company company { get; set; }
        public Franchise franchise { get; set; }
        public BillingEntityInfo billing_entity_info { get; set; }
        public List<string> order_methods { get; set; }
        public List<string> payment_methods { get; set; }
        public string phone_number { get; set; }
        public bool is_below_minimum_order_available { get; set; }
        public bool is_test_vendor { get; set; }
        public string business_type { get; set; }
        public string inflow_type { get; set; }
        public string vertical_type { get; set; }
        public AddressInfo vendor_address { get; set; }
        public string license_number { get; set; }
        public int takeout_minimum_minutes { get; set; }
        public List<category_set> category_set { get; set; }
        public List<object> partner_corporation_set { get; set; }
        public string custom_detailed_address { get; set; }
        public DateTime new_mark_date { get; set; }
        public string one_dish_offline_reason { get; set; }
        public string delivery_district_comment { get; set; }
        public DateTime additional_charged_date { get; set; }
        public string menu_leaflet_comment { get; set; }
        public string competitor_type { get; set; }
        public string competitor_id { get; set; }
        public bool has_liquor_sales_qualification { get; set; }
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
    public class VendorDescriptionInfo
    {
        public string description_method { get; set; }
        public string access_path_mobile { get; set; }
        public string access_path_web { get; set; }
        public string description { get; set; }
    }
    public class ContactableEmployee
    {
        public string employee_type { get; set; }
        public int vendor_id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public bool is_agree_recv_required_info { get; set; }
    }
    public class MobileRelay
    {
        public bool is_active { get; set; }
        public string phone_number { get; set; }
    }
    public class RelayMethod
    {
        public string method_type { get; set; }
        public object relaydevice {  get; set; }
        public List<object> contactableemployee_set { get; set; }
        public bool is_active { get; set; }
        public bool is_owner_using { get; set; }
    }

}
