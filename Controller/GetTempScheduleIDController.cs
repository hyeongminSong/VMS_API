using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class GetTempScheduleIDController
    {
        private readonly HttpClient _httpClient;

        public GetTempScheduleIDController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        // 임시 휴무일 텍스트를 통해 임시휴무일 Type ID 추출
        public async Task<int> GetTempScheduleTimeID(string scheduleName)
        {
            var getTempScheduleTimeIDEndpoint = "/temp-schedule-types/"; // 임시휴무일 Type ID EndPoint
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, getTempScheduleTimeIDEndpoint))
            {
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        JObject jObj = JObject.Parse(contentString);
                        JToken targetItem = jObj["results"].
                            FirstOrDefault(item => (string)item["name"] == scheduleName);

                        if (targetItem != null)
                        {
                            return (int)targetItem["id"];
                        }
                        else
                        {
                            return 0;
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Response status: {response.StatusCode}");
                        return 0;
                    }
                }
            }
        }
    }
}
