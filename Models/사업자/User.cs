using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models.사업자
{
    public class User
    {
        public int id { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(1)]
        [RegularExpression("^[-_.a-z0-9]{5,20}$")]
        public string username { get; set; }

        public string user_status { get; set; }

        [Required]
        [MaxLength(64)]
        [RegularExpression("^[ㄱ-힣\\w]+$")]
        public string name { get; set; }

        [MaxLength(256)]
        public string name_eng { get; set; }

        public string name_kor_eng { get; set; }

        [Required]
        [MaxLength(128)]
        [MinLength(1)]
        public string password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(254)]
        public string email { get; set; }

        public string email_certification_datetime { get; set; }

        [Required]
        [MaxLength(15)]
        [MinLength(1)]
        [RegularExpression("^(0[0-9]{1,2})-?([0-9]{3,4})-?([0-9]{4})$")]
        public string phone { get; set; }

        public string inflow_type { get; set; }

        public string nationality { get; set; }

        public string gender { get; set; }

        public string birth_date { get; set; }

        public string birth_date_str { get; set; }

        public string phone_certi_datetime { get; set; }

        public string self_certi_type { get; set; }

        public string certi_datetime { get; set; }

        [MaxLength(64)]
        public string certi_number { get; set; }

        public string issue_datetime { get; set; }

        [MaxLength(16)]
        public string certi_serial_code { get; set; }

        public string created_datetime { get; set; }

        public string modified_datetime { get; set; }

        public int created_by { get; set; }

        [MinLength(1)]
        public string created_by_slug { get; set; }

        public int modified_by { get; set; }

        [MinLength(1)]
        public string modified_by_slug { get; set; }

        public string staff_modified_datetime { get; set; }

        public string last_modified_by_slug { get; set; }

        public string last_modified_datetime { get; set; }

        public string owner_created_by_slug { get; set; }

        public string owner_modified_by_slug { get; set; }

        public string owner_modified_datetime { get; set; }

        public UserTermsInfo user_terms_info { get; set; }

        public UserFile[] userfile_set { get; set; }

        public string self_identify_type { get; set; }

        public string identify_datetime { get; set; }
    }

    public class UserTermsInfo
    {
        public string agree_terms_of_use_datetime { get; set; }

        public string agree_collection_personal_info_datetime { get; set; }

        public string agree_vms_terms_of_use_datetime { get; set; }

        public string agree_e_commerce_datetime { get; set; }

        public bool is_agree_recv_ad_info { get; set; }

        public string agree_recv_ad_info_datetime { get; set; }

        public bool is_agree_recv_ad_news_letter { get; set; }

        public string agree_recv_ad_news_letter_datetime { get; set; }

        public bool is_agree_bizcenter_terms_of_use { get; set; }

        public string agree_bizcenter_terms_of_use_datetime { get; set; }

        public bool is_agree_support_dhk { get; set; }

        public string agree_support_dhk_datetime { get; set; }

        public bool is_agree_delegate_info { get; set; }

        public string agree_delegate_info_datetime { get; set; }

        public bool is_agree_recommend_ad { get; set; }

        public string agree_recommend_ad_datetime { get; set; }

        public bool is_agree_delegate_recommend_ad { get; set; }

        public string agree_delegate_recommend_ad_datetime { get; set; }

        public bool is_agree_yotimedeal { get; set; }

        public string agree_yotimedeal_datetime { get; set; }

        public string agree_yotimedeal_datetime_approval { get; set; }

        public bool is_agree_delegate_yotimedeal { get; set; }

        public string agree_delegate_yotimedeal_datetime { get; set; }
    }
    public class UserFile
    {
        public int id { get; set; }

        public string file_path { get; set; }

        public string filename { get; set; }

        public string file_type { get; set; }

        public int user { get; set; }

        public int? created_by { get; set; }

        public string created_by_slug { get; set; }

        public string created_datetime { get; set; }

        public string owner_created_by_slug { get; set; }
    }
}
