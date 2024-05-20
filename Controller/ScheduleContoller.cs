using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp1.Models;

namespace ConsoleApp1.Controller
{
    public class ScheduleContoller
    {
        private readonly TempSchedule _tempSchedule = new TempSchedule();
        private readonly WorkingSchedule _workingSchedule = new WorkingSchedule();
        private readonly HolidaySchedule _holidaySchedule = new HolidaySchedule();
        private readonly SchedulePause _schedulePause = new SchedulePause();
        private readonly TempHoliday _tempHoliday = new TempHoliday();

        private static HttpClient _httpClient;

        public ScheduleContoller(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
        }

        ~ScheduleContoller() { }

        private class TempHoliday
        {
            //영업중지 사유 목록 조회
            public async Task<JObject> GetTempScheduleTypesReceiver(string name)
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", name },
                    // Add more parameters as needed
                };
                var Endpoint = "/temp-schedule-types/";

                string queryString = QueryStringBuilder.BuildQueryString(parameters);
                Endpoint += queryString;

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
            //임시 휴무일 조회
            public async Task<JObject> GetTempHolidayReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/temp-holiday-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
            //임시 휴무일 생성/수정/삭제
            public async Task<JObject> UpdateTempHolidayReceiver(int vendorID, TempHolidayScheduleMeta[] tempHolidayScheduleMetas)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(tempHolidayScheduleMetas, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/temp-holiday-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
                {
                    Content = jsonContent
                })
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }
        private class TempSchedule
        {
            //임시 영업시간 조회
            public async Task<JObject> GetTempScheduleReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/temp-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
            //임시 영업시간 수정
            public async Task<JObject> UpdateTempScheduleReceiver(int vendorID, TempScheduleMeta[] tempScheduleMetas)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                var temp_schedule_metas = new
                {
                    temp_schedule_meta = tempScheduleMetas
                };
                string jsonString = JsonConvert.SerializeObject(temp_schedule_metas, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/temp-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
                {
                    Content = jsonContent
                })
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }

        private class WorkingSchedule
        {
            //영업시간 조회
            public async Task<JObject> GetRegularScheduleReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
            //영업시간 수정
            public async Task<JObject> UpdateRegularScheduleReceiver(int vendorID, RegularScheduleMeta[] RegularScheduleMetas)
            {
                var regular_schedule_metas = new
                {
                    regular_schedule_meta = RegularScheduleMetas
                };

                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                };
                string jsonString = JsonConvert.SerializeObject(regular_schedule_metas, settings);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var Endpoint = string.Format("/vendor/{0}/regular-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("PATCH"), Endpoint)
                {
                    Content = jsonContent
                })
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }
        private class HolidaySchedule
        {
            //정기 휴무일 조회
            public async Task<JObject> GetHolidayScheduleReceiver(int vendorID)
            {
                var Endpoint = string.Format("/vendor/{0}/holiday-schedule-metas/", vendorID);

                using (var requestMessage = new HttpRequestMessage(new HttpMethod("GET"), Endpoint))
                {
                    try
                    {
                        using (var response = await _httpClient.SendAsync(requestMessage))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var contentString = await response.Content.ReadAsStringAsync();
                                return JObject.Parse(contentString);
                            }
                            else
                            {
                                Console.WriteLine($"Response status: {response.StatusCode}");
                                Console.WriteLine($"Response body: {await response.Content.ReadAsStringAsync()}");
                                return null;
                            }
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Request error: {ex.Message}");
                        // 예외 처리를 위한 로직 추가
                        return null;
                    }
                }
            }
        }
        private class SchedulePause
        {

        }

        public async Task<JObject> GetTempScheduleTypes(string name)
        {
            JObject getTempScheduleTypesObj = await _tempHoliday.GetTempScheduleTypesReceiver(name);
            return getTempScheduleTypesObj;
        }
        public async Task<JObject> GetTempHoliday(int vendorID)
        {
            JObject GetTempHolidayObj = await _tempHoliday.GetTempHolidayReceiver(vendorID);
            return GetTempHolidayObj;
        }
        public async Task<JObject> UpdateTempHoliday(int vendorID, TempHolidayScheduleMeta tempHolidayScheduleMeta)
        {
            TempHolidayScheduleMeta[] tempHolidayScheduleMetas = new TempHolidayScheduleMeta[] { tempHolidayScheduleMeta };
            JObject UpdateTempHolidayObj = await _tempHoliday.UpdateTempHolidayReceiver(vendorID, tempHolidayScheduleMetas);
            return UpdateTempHolidayObj;
        }
        public async Task<JObject> GetHolidaySchedule(int ticketID)
        {
            JObject getHolidayScheduleObj = await _holidaySchedule.GetHolidayScheduleReceiver(ticketID);
            return getHolidayScheduleObj;
        }
        public async Task<JObject> GetTempSchedule(int vendorID)
        {
            JObject GetTempScheduleObj = await _tempSchedule.GetTempScheduleReceiver(vendorID);
            return GetTempScheduleObj;
        }
        public async Task<JObject> UpdateTempSchedule(int vendorID, TempScheduleMeta tempScheduleMeta)
        {
            TempScheduleMeta[] tempScheduleMetas = new TempScheduleMeta[] { tempScheduleMeta };
            JObject UpdateTempScheduleObj = await _tempSchedule.UpdateTempScheduleReceiver(vendorID, tempScheduleMetas);
            return UpdateTempScheduleObj;
        }
        //정규 영업시간
        public async Task<JObject> GetWorkingSchedule(int vendorID)
        {
            JObject getWorkingScheduleObj = await _workingSchedule.GetRegularScheduleReceiver(vendorID);
            return getWorkingScheduleObj;
        }
        public async Task<JObject> UpdateWorkingSchedule(int vendorID, RegularScheduleMeta[] regularScheduleMetas)
        {
            JObject updateWorkingScheduleObj = await _workingSchedule.UpdateRegularScheduleReceiver(vendorID, regularScheduleMetas);
            return updateWorkingScheduleObj;
        }

    }
}
