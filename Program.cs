using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using System;
using System.Data;
using System.Threading.Tasks;

namespace ConsoleApp1
{

    class TEST_Program
    {
        public static DataTable CreateTempTable()
        {
            DataTable dataTable = new DataTable();
            DataTable workingTable = new WorkingTimeTable().CreateParseDataTable();

            // DataColumn 추가
            dataTable.Columns.Add("weekday", typeof(string));
            dataTable.Columns.Add("opening_start_time", typeof(string));
            dataTable.Columns.Add("opening_end_time", typeof(string));
            dataTable.Columns.Add("break_time", typeof(string[]));


            // DataRow 추가
            dataTable.Rows.Add("Mon", "10:00", "23:00", new string[] { });
            dataTable.Rows.Add("Tue", "10:00", "23:00", new string[] { });
            dataTable.Rows.Add("Wed", "10:00", "23:00", new string[] { });
            dataTable.Rows.Add("Thu", "10:00", "23:00", new string[] { });
            dataTable.Rows.Add("Fri", "10:00", "23:00", new string[] { });
            dataTable.Rows.Add("Sat", "10:00", "23:00", new string[] { });

            /*foreach (DataRow row in dataTable.Rows)
            {
                List<Models.TimeSpan> BreakTimeTimeSpanList = new List<Models.TimeSpan> { };           
                foreach (string breakTimeString in (string[])row["break_time"])
                {
                    BreakTimeTimeSpanList.Add(new Models.TimeSpan
                    {
                        start_time = breakTimeString.Split('-').First(),
                        end_time = breakTimeString.Split('-').Last()
                    });
                }
                workingTable.Rows.Add(row["weekday"],
                    new Models.TimeSpan
                    { start_time = (string)row["opening_start_time"], end_time = (string)row["opening_end_time"] },
                    BreakTimeTimeSpanList.ToArray()
                    );
            }*/

            return dataTable;
        }

        static async Task Main(string[] args)
        {
            UserData user = new UserData
            {
                Username = "heejin.park",
                Password = "qkrgmlwls1106!",
                Service_name = "VMS"
            };

            string url = "https://staging-ceo-portal-api.yogiyo.co.kr/";
            //string url = "https://ceo-portal-api.yogiyo.co.kr/";
            Config config = new Config(url);
            config = await new TokenController(config, user).GetAccessToken();

            try
            {
                /*AddTempScheduleTimeBody addTempScheduleTimeBody = new AddTempScheduleTimeBody()
                {
                    method = "POST",
                    order_type = "delivery",
                    description = "API TEST DESCRIPTION",
                    start_date = DateTime.Today.ToString("yyyy-MM-dd"),
                    end_date = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd")                   
                };

                Console.WriteLine(await new AddTempScheduleTimeController(config).AddTempSchedule(1001198,
                    "높은 주문 실패율로 인한 주문접수 불가",
                    addTempScheduleTimeBody));

                DeleteTempScheduleTimeBody deleteTempScheduleTimeBody = new DeleteTempScheduleTimeBody()
                {
                    method = "DELETE",
                    order_type = "delivery"
                };

                Console.WriteLine(await new DeleteTempScheduleTimeController(config).
                    DeleteTempSchedule(1001198, "높은 주문 실패율로 인한 주문접수 불가",deleteTempScheduleTimeBody));*/
                Console.WriteLine(config.Token);
                DataTable temp = CreateTempTable();
                //Console.WriteLine(await new AddWorkingScheduleController(config).AddWorkingSchedule(1001198, datatable));
                /*DataTable data = await new WorkingScheduleController(config).UpdateWorkingSchedule(1001201, new WorkingTimeTable().ParsingDataTable(temp));
                data = new WorkingTimeTable().unParsingDataTable(data);
                data = new WorkingTimeTable().ParsingDataTable(data);*/
                Console.WriteLine(await new GetVendorBillingEntitiesContoller(config).GetVendorBillingEntities(1001201, "franchise"));

                Console.WriteLine(new CommissionContractController(config).AddCommissionContract(
                    1001201, "franchise",
                    false, "오이시쿠나레모에모에큥",
                    true, "오이시VD", "VD",
                    "indirect", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));
                Console.WriteLine(config.Token);
                /* DataTable result = new WorkingTimeTable().unParsingDataTable(
                     await new WorkingScheduleController(config).
                     UpdateWorkingSchedule(1001201,
                     new WorkingTimeTable().ParsingDataTable(temp)));
                 Console.WriteLine(result);*/



                /*var vendortablePath = await new VendorDownloadController(config).
                    GetVerdorCSVFile(Path.Combine(Environment.CurrentDirectory, "vendortest.csv"));
                Console.WriteLine(vendortablePath);
                var onboardingtickettablePath = await new OnboardingTaskDownloadController(config).
                    GetTaskCSVFile(Path.Combine(Environment.CurrentDirectory, "onboardingtasktest.csv"));

                var maintainingtickettablePath = await new MaintainingTaskDownloadController(config).
                    GetTaskCSVFile(Path.Combine(Environment.CurrentDirectory, "maintainingtasktest.csv"));*/

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
