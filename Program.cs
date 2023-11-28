using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using static ConsoleApp1.Controller.CompanyController;

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
                /*Username = "heejin.park",
                Password = "qkrgmlwls1106!",*/
                Username = "hyeongmin.song",
                Password = "q1w2e3r$$",               
                Service_name = "VMS"
            };

            string url = "https://e-staging-ceo-portal-api.yogiyo.co.kr/";
            //string url = "https://staging-ceo-portal-api.yogiyo.co.kr/";
            //string url = "https://ceo-portal-api.yogiyo.co.kr/";
            Config config = new Config(url);
            config = await new TokenController(config, user).GetAccessToken();

            try
            {
                Console.WriteLine(config.Token);
                //Console.WriteLine(await new VendorController(config).GetVendor(1001376));


                ////////////////////////////////////////////////////////////////////////////////////////////////////
                /*//카테고리 객체 호출 API
                category_set categoryToken = await new VendorController(config).GetCategoryToken("야식");
                //기등록 가게 조회 API
                JObject vendorObj = await new VendorController(config).GetVendor(1001201);
                //기등록 가게 -> 기등록 카테고리 Obj 저장
                JArray categoryArr = (JArray)vendorObj["category_set"];
                //기등록 카테고리 Obj -> 카테고리 객체 리스트 변경
                List<category_set> categoryTokenList = categoryArr.Select(j => new category_set()
                {
                    id = (int)j["id"],
                    name = (string)j["name"],
                    category_type = (string)j["category_type"],
                }).ToList();

                categoryTokenList.Add(categoryToken);
                Console.WriteLine(categoryTokenList.ToArray());

                //가게 기본 정보 업데이트 API
                await new VendorController(config).UpdateVendor(1001201, new VendorCategoryUpdate()
                {
                    category_set = categoryTokenList.ToArray()
                });*/
                ////////////////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                /*int companyID = await new CompanyController(config).GetCompanyID("333-74-33333");
                int terminationReasonID = await new TerminationVendorController(config).GetTerminationReasonID("높은 주문 실패율(킥아웃)");
                await new TerminationVendorController(config).
                    CreateTerminationVendor(companyID, "333-74-33333", 1001358, terminationReasonID, DateTime.Today.AddDays(10).ToString("yyyy-MM-dd"));*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                /* //정산 주체 API
                 int billingTypeID = await new VendorController(config).GetVendorBillingEntities(1001201, "franchise");
                 //프랜차이즈 ID 조회 API
                 int franchisesID = await new FranchisesController(config).GetFranchisesID(false, "오이시쿠나레모에모에큥");
                 //이용료 ID 조회 API
                 int commissionID = await new CommissionController(config).GetCommissionID(true, "VD", franchisesID, "오이시VD");

                 //이용료 생성 API
                Console.WriteLine(await new CommissionController(config).AddCommissionContract(
                    1001358, billingTypeID, commissionID, true,
                    "indirect", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));

                 //계약서 생성 API
                Console.WriteLine(await new ContractAuditController(config).CreateContractAudit(1001358, DateTime.Today.AddDays(10).ToString("yyyy-MM-dd")));

                 //세일즈 심사 승인 API
                Console.WriteLine(await new ContractAuditController(config).RequestSalesApprove(1001201, 3810));

                 //계약서 영업 개시일 업데이트 API
                Console.WriteLine(await new ContractAuditController(config).UpdateContractAudit(1001201, 3810, 588,
                    DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));

                 //사장님 승인 요청 API
                Console.WriteLine(await new ContractAuditController(config).RequestOwnerApprove(1001201, 3810));*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                // 가게 등록 TEST
                int companyID = await new CompanyController(config).GetCompanyID("888-64-88888");
                AddressInfo addressInfo = await new VendorController(config).GetVendorAddressInfo("경기도 고양시 일산동구 숲속마을로  50-58 보민프라자");
                Console.WriteLine(await new VendorController(config).CreateVendor(new Vendor
                {
                    name = "API로만든가게3",
                    company = new Models.Company
                    {
                        id = companyID
                    },
                    vendor_address = addressInfo,
                    vertical_type = "restaurant",
                    business_type = "no_info",
                    license_number = "20230260108"
                }));


                ////////////////////////////////////////////////////////////////////////////////////////////////////




                Console.WriteLine(config.Token);



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
