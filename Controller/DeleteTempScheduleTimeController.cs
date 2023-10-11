using ConsoleApp1.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ConsoleApp1.Controller
{
    internal class DeleteTempScheduleTimeController
    {
        private readonly HttpClient _httpClient;
        //현재 가게에 등록된 임시 휴무일의 ID를 얻기 위한 Controller
        private readonly GetTempScheduleObjectController getTempScheduleObjectController;
        public DeleteTempScheduleTimeController(Config config)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {config.Token}");
            //현재 가게에 등록된 임시 휴무일의 ID를 얻기 위한 Controller 생성
            getTempScheduleObjectController = new GetTempScheduleObjectController(config);

        }
        public async Task<bool> DeleteTempSchedule(int vendorID, string scheduleName, DeleteTempScheduleTimeBody deleteTempScheduleObj)
        {
            //현재 가게에 등록된 임시 휴무일의 ID 저장
            int TempScheduleObjectID = await getTempScheduleObjectController.GetTempScheduleObjectID(vendorID, scheduleName);
            //임시휴무일 삭제 DTO에 삭제할 임시 휴무일 ID 저장
            deleteTempScheduleObj.id = TempScheduleObjectID;

            var setTempScheduleTimeIDEndpoint = string.Format("/vendor/{0}/temp-holiday-schedule-metas/", vendorID);

            var jsonContent = JsonContent.Create(new DeleteTempScheduleTimeBody[] { deleteTempScheduleObj });
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
