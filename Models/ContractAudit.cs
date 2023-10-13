using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class ContractAudit
    {
        public int vendor {  get; set; }
        public string inflow_type { get; set; }
        public string contract_type {  get; set; }
        public string contract_date { get; set;}
        public string contract_manager { get; set;}
        public bool is_requested_first_onboarding { get; set; }
        public bool is_requested_next_onboarding { get; set; }

    }

    public class ContractAuditPatch {
        public int vendor { get; set; }
        //public Contract_manager contract_manager { get; set; }
        public string open_date {  get; set; }
    }


    public class ContractAuditSalesApprove
    {
        public string contract_status { get; set; }
    }

    public class ContractAuditOwnerRequest
    {
        public string contract_status { get; set; }
    }

    public class Contract_manager
    {
        public int id { get; set; }
    }
}
