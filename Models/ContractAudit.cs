using System.Collections.Generic;

namespace ConsoleApp1.Models
{
    public class ContractAudit
    {
        public int vendor { get; set; } // 가게명
        public string inflow_type { get; set; } //유입 채널
        public string contract_type { get; set; } //계약 구분
        public string contract_date { get; set; } //계약일
        public int contract_manager { get; set; } //세일즈 담당자
        public bool is_requested_first_onboarding { get; set; } //최초 온보딩
        public bool is_requested_next_onboarding { get; set; } //수정 온보딩
        public bool is_requested_pos_code { get; set; } //POS코드 연결
        public bool is_requested_template_menu { get; set; } //템플릿 메뉴 등록
        public List<Contract> commission_contract_set { get; set; } //주문당 이용료 정보
        public List<Contract> zero_commission_contract_set { get; set; } //월정액 이용료 정보
        public List<Contract> additional_fee_set { get; set; } //추가 과금 정보
        public string open_date { get; set; } //영업 개시일
    }

    public class Contract
    {
        public int id { get; set; } //계약서 ID
    }

    public class ContractAuditPatch
    {
        public int vendor { get; set; } //가게명
        public string open_date { get; set; } //영업 개시일
    }

    public class ContractAuditSalesApprove
    {
        public string contract_status { get; set; }
    }

    public class ContractAuditOwnerRequest
    {
        public string contract_status { get; set; }
    }
}
