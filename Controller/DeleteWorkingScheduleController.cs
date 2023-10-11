using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class DeleteWorkingScheduleController
    {
        private readonly HttpClient _httpClient;
        public DeleteWorkingScheduleController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");

        }

        public regular_schedule_meta[] CreateJsonContent(DataTable table)
        {
            List<regular_schedule_meta> regularScheduleMetaList = new List<regular_schedule_meta>();
            foreach (DataRow row in table.Rows)
            {
                regular_schedule_meta regularScheduleMeta = new regular_schedule_meta
                {
                    id = (int)row["id"],
                    order_type = "delivery",
                    method = "DELETE"

                };
                regularScheduleMetaList.Add(regularScheduleMeta);

            }
            return regularScheduleMetaList.ToArray();
        }
        public async Task<bool> DeleteWorkingSchedule(int vendorID, DataTable table)
        {
            var AddWorkingScheduleEndpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);
            var jsonContent = JsonContent.Create(new WorkingScheduleBody { regular_schedule_meta = CreateJsonContent(table) });
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), AddWorkingScheduleEndpoint)
            {
                Content = jsonContent
            })
            {
                Console.WriteLine(await requestMessage.Content.ReadAsStringAsync());
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
