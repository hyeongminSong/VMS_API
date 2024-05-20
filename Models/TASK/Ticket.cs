using ConsoleApp1.Models.사업자;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.TASK
{
    public class Ticket
    {
        public int id { get; set; }
        public TicketType ticket_type { get; set; }
        public string status { get; set; }
        public string description { get; set; }
        public Assignee assignee { get; set; }
        public int? parent { get; set; }
        public AssignedOrganization assigned_organization { get; set; }
        public bool? is_success { get; set; }
        public string success_reason { get; set; }
        public string rejected_reason { get; set; }
        public TargetUser target_user { get; set; }
        public NestedCompanyRelatedField target_company { get; set; }
        public TargetVendor target_vendor { get; set; }
        public string title { get; set; }
        public bool? is_active { get; set; }
        public string start_process_datetime { get; set; }
        public string need_to_process_start_date { get; set; }
        public string need_to_process_end_date { get; set; }
    }

    public class TicketType
    {
        public string ticket { get; set; }
        public int id { get; set; }
        public bool is_active { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public string created_by { get; set; }
        public string modified_by { get; set; }
        public string staff_modified_datetime { get; set; }
        public string type_depth1 { get; set; }
        public string type_depth2 { get; set; }
        public int ordering_num { get; set; }
        public AssignedOrganization default_assigned_organization { get; set; }
        public List<string> rejected_reasons { get; set; }
    }

    public class Assignee
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }
    }

    public class AssignedOrganization
    {
        public int id { get; set; }
        public string depth1 { get; set; }
        public string depth2 { get; set; }
        public string depth3 { get; set; }
    }

    public class TargetUser
    {
        public int id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string name_eng { get; set; }
    }

    public class NestedCompanyRelatedField
    {
        public int id { get; set; }
        public string name { get; set; }
        public PrincipalCompany principal_company { get; set; }
        public string subordinate_num { get; set; }
        public string created_datetime { get; set; }
        public string modified_datetime { get; set; }
        public User user { get; set; }
    }

    public class TargetVendor
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class TicketAssignee
    {
        public Assignee assignee { get; set; }
    }

    public enum TicketStatus
    {
        ready,
        in_progress,
        complete,
        pending
    }
}
