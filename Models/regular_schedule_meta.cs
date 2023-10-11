namespace ConsoleApp1.Models
{
    public class regular_schedule_meta
    {
        public int id { get; set; }
        public string order_type { get; set; }
        public string schedule_type { get; set; }
        public string weekday { get; set; }
        public TimeSpan opening_time { get; set; }
        public TimeSpan[] break_time { get; set; }
        public string method { get; set; }
    }
}
