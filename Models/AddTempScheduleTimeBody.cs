namespace ConsoleApp1.Models
{
    internal class AddTempScheduleTimeBody
    {
        public string method { get; set; }
        public string order_type { get; set; }
        public TempScheduleType temp_schedule_type { get; set; }
        public string description { get; set; }
        public string start_date { get; set; }
        public string end_date { get; set; }

        public AddTempScheduleTimeBody()
        {
        }
    }
}
