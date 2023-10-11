using ConsoleApp1.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class AddTempScheduleTimeController
    {
        private readonly HttpClient _httpClient;
        private readonly GetTempScheduleIDController tempScheduleTimeIDController;
        public AddTempScheduleTimeController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
            tempScheduleTimeIDController = new GetTempScheduleIDController(config);

        }
        public async Task<bool> AddTempSchedule(int vendorID, string scheduleName, AddTempScheduleTimeBody setTempScheduleObj)
        {
            int tempHolidayScheduleID = await tempScheduleTimeIDController.GetTempScheduleTimeID(scheduleName);
            setTempScheduleObj.temp_schedule_type = new TempScheduleType(tempHolidayScheduleID);

            var setTempScheduleTimeIDEndpoint = string.Format("/vendor/{0}/temp-holiday-schedule-metas/", vendorID);

            var jsonContent = JsonContent.Create(new AddTempScheduleTimeBody[] { setTempScheduleObj });
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), setTempScheduleTimeIDEndpoint)
            {
                Content = jsonContent
            })
            {
                using (var response = await _httpClient.SendAsync(requestMessage))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        return true;

                    }
                    else
                    {
                        Console.WriteLine($"Response status: {response.StatusCode}");
                        return false;
                    }
                }
            }
        }
    }
}
