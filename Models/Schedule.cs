using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class TimeSpan
    {
        public string start_time { get; set; }
        public string end_time { get; set; }
    }
    public class RegularScheduleMeta
    {
        public int id { get; set; }
        public string order_type { get; set; }
        public string vendor_id { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string schedule_type { get; set; }
        public string weekday { get; set; }
        public TimeSpan opening_time { get; set; }
        public TimeSpan[] break_time { get; set; }
        public string method { get; set; }
    }
    public class HolidayScheduleMeta
    {
        public int id { get; set; }
        public int holiday_period { get; set; }
        public string weekday { get; set; }
        public string order_type { get; set; }
        public string vendor_id { get; set; }
        public string method { get; set; }
    }
    public class SchedulePause
    {
        public int id { get; set; }

        public int vendor_id { get; set; }

        public string order_type { get; set; }

        public string start_datetime { get; set; }

        public string end_datetime { get; set; }

        public int minutes { get; set; }

        public int day { get; set; }
    }
    public class TempScheduleMeta
    {
        public int id { get; set; }

        public string method { get; set; }

        public string order_type { get; set; }

        public string schedule_type { get; set; }

        public string temp_schedule_start_date { get; set; }

        public string temp_schedule_end_date { get; set; }

        public TimeSpan opening_time { get; set; }

        public TimeSpan[] break_time { get; set; }
    }

    public class TempScheduleType
    {
        public int id { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string staff_modified_datetime { get; set; }
        public string name { get; set; }
        public bool can_use_by_vms_vendor { get; set; }
        public bool can_use_by_vms_bulk_update { get; set; }
        public bool can_use_by_relayo { get; set; }
        public bool can_use_by_ceo_site { get; set; }
        public bool can_use_by_ceo_app { get; set; }
        public bool can_use_by_gowin { get; set; }
        public bool can_use_by_godroid { get; set; }
        public bool can_use_by_system { get; set; }
        public bool enable_disabled_by_owner { get; set; }
        public string info { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
    }

    public class TempHolidayScheduleMeta
    {
        public int id { get; set; }
        public string created_by_slug { get; set; }
        public string modified_by_slug { get; set; }
        public string owner_created_by_slug { get; set; }
        public string owner_modified_by_slug { get; set; }
        public string last_modified_by_slug { get; set; }
        public string last_modified_datetime { get; set; }
        public string method { get; set; }
        public TempScheduleType temp_schedule_type { get; set; }
        public string description { get; set; }
        public int vendor_id { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string staff_modified_datetime { get; set; }
        public string owner_modified_datetime { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string order_type { get; set; }
        public string bulk_update_tracking_id { get; set; }
        public int? created_by { get; set; }
        public int? modified_by { get; set; }
        public int? owner_created_by { get; set; }
        public int? owner_modified_by { get; set; }
    }
}
