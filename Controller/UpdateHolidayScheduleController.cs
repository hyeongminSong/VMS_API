/*using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.Controller
{
    internal class UpdateHolidayScheduleController
    {
        private readonly HttpClient _httpClient;
        public UpdateHolidayScheduleController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");

        }

        private holiday_schedule_meta[] CreateJsonContent(DataTable table)
        {
            List<holiday_schedule_meta> regularScheduleMetaList = new List<holiday_schedule_meta>();
            foreach (DataRow row in table.Rows)
            {
                holiday_schedule_meta regularScheduleMeta = new holiday_schedule_meta
                {
                    order_type = "delivery",
                    weekday = (string)row["weekday"],
                    method = "POST"

                };
                regularScheduleMetaList.Add(regularScheduleMeta);

            }
            return regularScheduleMetaList.ToArray();
        }

        private async Task<JObject> AddWorkingScheduleReceiver(int vendorID, JsonContent jsonContent)
        {
            var AddWorkingScheduleEndpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), AddWorkingScheduleEndpoint)
            {
                Content = jsonContent
            })
            {
                Console.WriteLine(await requestMessage.Content.ReadAsStringAsync());
                using (var response = await _httpClient.SendAsync(requestMessage))
                    if (response.IsSuccessStatusCode)
                    {
                        var contentString = await response.Content.ReadAsStringAsync();
                        return JObject.Parse(contentString);

                    }
                    else
                    {
                        Console.WriteLine($"Response status: {response.StatusCode}");
                        return null;
                    }
            }
        }
        public async Task<DataTable> AddWorkingSchedule(int vendorID, DataTable table)
        {
            var jsonContent = JsonContent.Create(new WorkingScheduleBody { regular_schedule_meta = CreateJsonContent(table) });
            JObject SearchWorkingScheduleobj = await AddWorkingScheduleReceiver(vendorID, jsonContent);

            foreach (var obj in SearchWorkingScheduleobj["regular_schedule_meta"])
            {
                Models.TimeSpan opening_time = new Models.TimeSpan
                {
                    start_time = (string)obj["opening_time"]["start_time"],
                    end_time = (string)obj["opening_time"]["end_time"]
                };
                List<Models.TimeSpan> break_time_list = new List<Models.TimeSpan>();

                if (obj["break_time"].Any())
                {
                    foreach (var break_time in obj["break_time"])
                    {
                        {
                            break_time_list.Add(new Models.TimeSpan
                            {
                                start_time = (string)break_time["start_time"],
                                end_time = (string)break_time["end_time"]
                            });
                        }
                    }

                }
                table.Rows.Add(obj["weekday"], opening_time, break_time_list.ToArray(), obj["id"]);
            }

            return table;
        }
    }
}
*/