using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class SearchWorkingScheduleController
    {
        private readonly HttpClient _httpClient;

        public SearchWorkingScheduleController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }
        public async Task<JObject> SearchWorkingScheduleReceiver(int vendorID)
        {
            var AddWorkingScheduleEndpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, AddWorkingScheduleEndpoint))
            {
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
        public async Task<DataTable> SearchWorkingSchedule(int vendorID)
        {
            DataTable table = new WorkingTimeTable().CreateParseDataTable();
            JObject SearchWorkingScheduleobj = await SearchWorkingScheduleReceiver(vendorID);

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