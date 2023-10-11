using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class GetTempScheduleObjectController
    {
        private readonly HttpClient _httpClient;
        //임시 휴무일 Type의 ID를 얻기 위한 Controller
        private readonly GetTempScheduleIDController tempScheduleTimeIDController;

        public GetTempScheduleObjectController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
            tempScheduleTimeIDController = new GetTempScheduleIDController(config);

        }

        public async Task<int> GetTempScheduleObjectID(int vendorID, string scheduleName)
        {
            // 임시 휴무일 텍스트를 통해 임시휴무일 Type ID 추출
            int tempHolidayScheduleID = await tempScheduleTimeIDController.GetTempScheduleTimeID(scheduleName);
            var getTempScheduleTimeObjectEndpoint = string.Format("/vendor/{0}/temp-holiday-schedule-metas/", vendorID); ; // Vendor Download EndPoint

            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, getTempScheduleTimeObjectEndpoint))
            {
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        JObject jObj = JObject.Parse(contentString);
                        JToken targetItem = jObj["temp_holiday_schedule_meta"].
                            FirstOrDefault(item => (int)item["temp_schedule_type"]["id"] == tempHolidayScheduleID);

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
