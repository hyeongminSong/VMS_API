namespace ConsoleApp1.Models
{
    public class Config
    {
        public string URL { get; set; }
        public string Token { get; set; }
        public Config(string URL)
        {
            this.URL = URL;
        }
    }
}
