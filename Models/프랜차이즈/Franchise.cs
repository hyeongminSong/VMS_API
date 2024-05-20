using System;
using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class Franchise
    {
        public string commission { get; set; }
        public string franchisestaffrelation { get; set; }
        public string staff { get; set; }
        public string vendor { get; set; }
        public string franchise { get; set; }
        public FranchiseExtra franchiseextra { get; set; }
        public string franchisecontract { get; set; }
        public string franchiseticket { get; set; }
        public string franchisenoticefranchiserelation { get; set; }
        public string franchisenotice { get; set; }
        public string user { get; set; }
        public int id { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string staff_modified_datetime { get; set; }
        public string name { get; set; }
        public string principal_company { get; set; }
        public string group { get; set; }
        public bool is_group { get; set; }
    }
    public class FranchiseExtra
    {
        public int franchise { get; set; }
        public string title { get; set; }
        public bool is_private_hall_available { get; set; }
        public string private_hall_name { get; set; }
        public bool is_relayo_available { get; set; }
        public bool is_catalogyo { get; set; }
        public bool is_yostore { get; set; }
        public string relayo_timeout_time { get; set; }
        public bool is_simultaneous_order_available { get; set; }
        public bool is_assured_phone_call_available { get; set; }
        public bool is_below_minimum_order_available { get; set; }
        public string od_rider_assignment_method { get; set; }
        public bool is_stock_available { get; set; }
        public string is_refund_exclude_commission { get; set; }
        public List<FranchiseDescriptionInfo> franchisedescriptioninfo_set { get; set; }
        public List<int> franchisefile_set { get; set; }
        public bool use_owner_manage_relay_method { get; set; }
        public bool use_owner_menu_image { get; set; }
        public bool use_owner_menu_self_management { get; set; }
        public bool use_owner_manage_delivery_fee { get; set; }
        public bool use_owner_takeout { get; set; }
        public bool use_owner_manage_schedule { get; set; }
        public bool use_owner_manage_price { get; set; }
        public bool use_owner_manage_advertisement { get; set; }
        public bool use_owner_manage_yotimedeal { get; set; }
        public bool use_owner_manage_discounts { get; set; }
        public bool use_owner_manage_coupon { get; set; }
    }

    public class FranchiseDescriptionInfo
    {
        public string description_type { get; set; }
        public string description_method { get; set; }
        public string access_path_mobile { get; set; }
        public string access_path_web { get; set; }
        public string description { get; set; }
    }
    public class FranchiseReadOnly
    {
        public int id { get; set; }
        public string name { get; set; }
        public int group { get; set; }
        public PrincipalCompany principal_company { get; set; }
        public string initial_contract_start_date { get; set; }
        public bool is_private_hall_available { get; set; }
        public bool is_yostore { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
    }
}
