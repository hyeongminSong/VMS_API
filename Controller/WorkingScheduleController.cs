using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ConsoleApp1.Controller
{
    internal class WorkingScheduleController
    {
        private readonly HttpClient _httpClient;
        List<regular_schedule_meta> regularScheduleMetaList = new List<regular_schedule_meta>();

        public WorkingScheduleController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        private List<regular_schedule_meta> CreateJsonContent(string weekday, Models.TimeSpan opening_time, Models.TimeSpan[] break_time, int id, string method)
        {
            regular_schedule_meta regularScheduleMeta = new regular_schedule_meta
            {
                order_type = "delivery",
                weekday = weekday,
                opening_time = opening_time,
                break_time = break_time,
                id = id,
                method = method
            };
            regularScheduleMetaList.Add(regularScheduleMeta);

            return regularScheduleMetaList;
        }

        private List<regular_schedule_meta> CreateJsonAddContent(DataTable Addtable)
        {
            foreach (DataRow row in Addtable.Rows)
            {
                regular_schedule_meta regularScheduleMeta = new regular_schedule_meta
                {
                    order_type = "delivery",
                    weekday = (string)row["weekday"],
                    opening_time = (Models.TimeSpan)row["opening_time"],
                    break_time = (Models.TimeSpan[])row["break_time"],
                    method = "POST"
                };
                regularScheduleMetaList.Add(regularScheduleMeta);

            }
            return regularScheduleMetaList;
        }
        private List<regular_schedule_meta> CreateJsonUpdateContent(DataTable UpdateTable)
        {
            foreach (DataRow row in UpdateTable.Rows)
            {
                regular_schedule_meta regularScheduleMeta = new regular_schedule_meta
                {
                    id = (int)row["id"],
                    order_type = "delivery",
                    weekday = (string)row["weekday"],
                    opening_time = (Models.TimeSpan)row["opening_time"],
                    break_time = (Models.TimeSpan[])row["break_time"],
                    method = "PATCH"

                };
                regularScheduleMetaList.Add(regularScheduleMeta);
            }
            return regularScheduleMetaList;
        }

        private List<regular_schedule_meta> CreateJsonDeleteContent(DataTable UpdateTable)
        {
            foreach (DataRow row in UpdateTable.Rows)
            {
                regular_schedule_meta regularScheduleMeta = new regular_schedule_meta
                {
                    id = (int)row["id"],
                    order_type = "delivery",
                    /*weekday = (string)row["weekday"],
                    opening_time = (Models.TimeSpan)row["opening_time"],
                    break_time = (Models.TimeSpan[])row["break_time"],*/
                    method = "DELETE"

                };
                regularScheduleMetaList.Add(regularScheduleMeta);
            }
            return regularScheduleMetaList;
        }
        private async Task<JObject> SearchWorkingScheduleReceiver(int vendorID)
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
        private async Task<JObject> UpdateWorkingScheduleReceiver(int vendorID, JsonContent jsonContent)
        {
            Console.WriteLine(JsonConvert.SerializeObject(jsonContent));
            var UpdateWorkingScheduleEndpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);
            using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), UpdateWorkingScheduleEndpoint)
            {
                Content = jsonContent
            })
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
            DataTable SearchResultTable = new WorkingTimeTable().CreateParseDataTable();
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
                SearchResultTable.Rows.Add(obj["weekday"], opening_time, break_time_list.ToArray(), obj["id"]);
            }
            return SearchResultTable;
        }
        public async Task<DataTable> UpdateWorkingSchedule(int vendorID, DataTable UpdateTable)
        {
            /* 
             * UpdateTable -> 변경할 영업시간 Table
             * CurrentTable -> 현재 등록된 영업시간 Table
             * UpdateMatchingTable -> 변경한 영업시간 Table 중, 현재 등록된 영업시간 Table => Update
            (현재 등록된 영업시간 <> 변경할 영업시간을 Join해서 등록된 영업시간 Object의 ID를 저장)
             * DeleteMatchingTable -> 현재 등록된 영업시간 Table 중, 변경할 영업시간 Table에 없는 영업시간 => Delete
             * AddMathcingTable -> 변경할 영업시간 Table 중, 현재 등록되지 않은 영업시간 Table => Add
            */
            DataTable UpdateResultTable = new WorkingTimeTable().CreateParseDataTable();

            DataTable CurrentTable = await SearchWorkingSchedule(vendorID);
            var UpdateMatchingRows = CurrentTable.AsEnumerable()
                .Join(UpdateTable.AsEnumerable(),
                CurrentRow => CurrentRow.Field<string>("weekday"),
                UpdateRow => UpdateRow.Field<string>("weekday"),
                (CurrentRow, UpdateRow) => new
                {
                    weekday = CurrentRow.Field<string>("weekday"),
                    opening_time = UpdateRow.Field<Models.TimeSpan>("opening_time"),
                    break_time = UpdateRow.Field<Models.TimeSpan[]>("break_time"),
                    id = CurrentRow.Field<int>("id")
                });

            var DeleteMatchingRows = CurrentTable.AsEnumerable()
                .Where(CurrentRow => !UpdateMatchingRows.Any(match => match.weekday == CurrentRow.Field<string>("weekday")))
                .Select(CurrentRow => new
                {
                    weekday = CurrentRow.Field<string>("weekday"),
/*                    opening_time = CurrentRow.Field<Models.TimeSpan>("opening_time"),
                    break_time = CurrentRow.Field<Models.TimeSpan[]>("break_time"),*/
                    id = CurrentRow.Field<int>("id")
                });

            foreach (var row in DeleteMatchingRows)
            {
                regularScheduleMetaList = CreateJsonContent(row.weekday, null, null, row.id, "DELETE");
            }

            //matching Table -> Update, unmatching Table -> Add               
            foreach (var row in UpdateMatchingRows)
            {
                regularScheduleMetaList = CreateJsonContent(row.weekday, row.opening_time, row.break_time, row.id, "PATCH");
            }

            var AddMatchingRows = UpdateTable.AsEnumerable()
            .Where(UpdateRow => !UpdateMatchingRows.Any(match => match.weekday == UpdateRow.Field<string>("weekday")))
             .Select(CurrentRow => new
             {
                 weekday = CurrentRow.Field<string>("weekday"),
                 opening_time = CurrentRow.Field<Models.TimeSpan>("opening_time"),
                 break_time = CurrentRow.Field<Models.TimeSpan[]>("break_time")
                 //id = CurrentRow.Field<int>("id")
             });

            foreach (var row in AddMatchingRows)
            {
                regularScheduleMetaList = CreateJsonContent(row.weekday, row.opening_time, row.break_time, 0, "POST");
            }

            var jsonContent = JsonContent.Create(new WorkingScheduleBody { regular_schedule_meta = regularScheduleMetaList.ToArray() });
            JObject UpdateWorkingScheduleobj = await UpdateWorkingScheduleReceiver(vendorID, jsonContent);
            foreach (var obj in UpdateWorkingScheduleobj["regular_schedule_meta"])
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
                UpdateResultTable.Rows.Add(obj["weekday"], opening_time, break_time_list.ToArray(), obj["id"]);
            }
            return UpdateResultTable;
        }
    }
}
