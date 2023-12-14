using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
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

            CompanyController companyController = new CompanyController(config);
            FranchisesController franchisesController = new FranchisesController(config);
            VendorController vendorController = new VendorController(config);
            CommissionController commissionController = new CommissionController(config);
            ContractAuditController contractAuditController = new ContractAuditController(config);

            try
            {
                Console.WriteLine("Bearer " + config.Token);
                //Console.WriteLine(await new VendorController(config).GetVendor(1001376));
                int vendorID = 1004012;
                string phone = "01012345678";

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //카테고리 객체 호출 API
                /*category_set categoryToken = await vendorController.GetCategoryToken("야식");
                //기등록 가게 조회 API
                JObject vendorObj = await vendorController.GetVendor(vendorID);
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
                Console.WriteLine(await vendorController.UpdateVendor(vendorID, new Vendor()
                {
                    category_set = categoryTokenList
                }));*/

                //영업시간 텍스트 입력
                /*JArray vendorDescriptionInfoObj = await vendorController.GetVendorDescriptionsInfo(vendorID);
                JToken targetItem = vendorDescriptionInfoObj
                    .FirstOrDefault(item => (string)item["description_type"] == "opening_date");
                if (targetItem != null)
                {
                    int descriptionID = (int)targetItem["id"];
                    Console.WriteLine(await vendorController.UpdateVendorDescriptionsInfo(vendorID, descriptionID, "API로 바꾼 영업시간 텍스트"));
                }*/

                //주문전달수단
                /*Console.WriteLine(await vendorController.GetVendorContactableEmployees(vendorID));
                JObject vendorContactableEmployeesObj = await vendorController.GetVendorContactableEmployees(vendorID);
                string[] contactableEmployeesArr = vendorContactableEmployeesObj["results"]
                    .Select(token => token["phone"].ToString()).ToArray();
                if (!contactableEmployeesArr.Contains(phone))
                {
                    //운영자 추가
                    Console.WriteLine(await vendorController.CreateVendorContactableEmployees(vendorID, phone));
                    //주문전달수단(SMS) 추가
                    Console.WriteLine(await vendorController.UpdateVendorMobileRelayMethods(vendorID, phone));
                }*/

                /*Console.WriteLine(await vendorController.UpdateVendor(vendorID, new Vendor()
                {
                    competitor_id = "메뉴 어쩌구 CM"
                }));*/
                Console.WriteLine(await vendorController.CheckVendorDuplicateName("API로만든가게1129182745"));
                Console.WriteLine(await vendorController.GetVendorSearchByCompanyNumber("API로만든가게1129182745", "888-64-88888"));


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

                /*string franchiseType = "franchise";
                string contractName = "요기팩 기본 [제휴 5%]";
                string orderType = "VD";
                int managerId = 200;

                int companyID = await companyController.GetCompanyID("888-64-88888");
                int franchisesID = await franchisesController.GetFranchisesID(false, "CU");
                AddressInfo addressInfo = await vendorController.GetAddressInfo("경기도 고양시 일산동구 숲속마을로  50-58 보민프라자", "");

                JObject vendorObj = await vendorController.CreateVendor(new Vendor
                {
                    name = "API로만든가게" + DateTime.Now.ToString("MMddHHmmss"),
                    company = new Models.Company
                    {
                        id = companyID
                    },
                    franchise = new Franchise { id = franchisesID },
                    vendor_address = addressInfo,
                    vertical_type = "restaurant",
                    business_type = "no_info",
                    license_number = string.Empty
                });
                Console.WriteLine(vendorObj);

                int vendorId = (int)vendorObj["id"];

                int billingTypeID = await vendorController.GetVendorBillingEntities(vendorId, franchiseType);
                int commissionID = await commissionController.GetCommissionID(true, orderType, franchisesID, contractName);

                //int vendor_id, int billingTypeID, int commissionID, bool is_alliance, string franchiseType, string start_date
                int createContractID = await commissionController.AddCommissionContract(
                    vendorId, billingTypeID, commissionID, true,
                    "indirect", DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"));


                int createContractAuditId = await contractAuditController.CreateContractAudit(vendorId, managerId, createContractID,
                    DateTime.Today.AddDays(10).ToString("yyyy-MM-dd"));

                //세일즈 심사 승인 API
                Console.WriteLine(contractAuditController.RequestSalesApprove(vendorId, createContractAuditId));

                //계약서 영업 개시일 업데이트 API
                Console.WriteLine(contractAuditController.UpdateContractAudit(vendorId, createContractAuditId,
                    DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));

                //사장님 승인 요청 API
                Console.WriteLine(await contractAuditController.RequestOwnerApprove(vendorId, createContractAuditId));*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //사업자 정보 등록 TEST
                /*string path = "C:\\Users\\A202111078\\Desktop\\GSTHEFRESH-파주와동점 사업자등록증.pdf.gpg";
                                
                string companyNumber = "625-13-01322";
                string companyName = "세븐일레븐 안산본오그린점";
                string companyEngName = string.Empty;
                string corpName = "";
                string corpEngName = "";
                string address = "경상북도 구미시 송원서로8길  15";
                string addressDetail = "1층, 102~103호";
                string representative = "김수빈";
                string phone = "01058638868";
                string businessType = "ZZZZZ";
                // 법인 -> 주주명부, 개인 -> 여
                string proprietorVerifiMethod = "여";

                JObject InquiryCompanyInfoObj = await companyController.GetInquiryCompanyInfo(companyNumber);
                string companyNumberCode = (string)InquiryCompanyInfoObj["company_number_code"];
                string companyStatus = (string)InquiryCompanyInfoObj["company_status"];
                string companyType = (string)InquiryCompanyInfoObj["company_type"];

                AddressInfo addressInfo = await vendorController.GetAddressInfo(address, addressDetail);
                Console.WriteLine(addressInfo);
                //Console.WriteLine(await companyController.GetCompanyID(companyNumber));
                PrincipalCompany principalCompanyObj = new PrincipalCompany
                {
                    company_number = companyNumber,
                    company_number_code = companyNumberCode,
                    company_status = companyStatus,
                    business_type = businessType,
                    name = companyName,
                    name_eng = companyEngName,
                    corp_name = corpName,
                    corp_name_eng = corpEngName,
                    address = addressInfo,
                    representative_set = new List<Representative> { new Representative { name = representative } },
                    corp_phone = phone,
                    proprietor_verifi_method = proprietorVerifiMethod,
                };

                //비영리법인
                if (companyType == "non_profit_corp")
                {
                    principalCompanyObj.estimated_purpose = "자선";
                    principalCompanyObj.estimated_purpose_file_type = "정관";
                }

                Console.WriteLine(await companyController.CreatePrincipalCompany(principalCompanyObj));
                Console.WriteLine(await companyController.CreateCompanyFiles(240, 3, path));
                //Console.WriteLine(await companyController.UpdatePrincipalCompany(241, principalCompanyObj));*/

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