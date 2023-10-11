namespace ConsoleApp1.Models
{
    public class commission
    {
        public int Id { get; set; }
    }

    public class billing_type_info
    {
        public int Id { get; set; }
    }
    
    public class CommissionContract
    {
        public int Id { get; set; }
        public int vendor { get; set; }
        public bool is_alliance { get; set; }
        public int commission { get; set; }
        public int billing_entity_info { get; set; }
        public string franchise_type {  get; set; }
        public string commission_start_date { get; set; }
    }
}
