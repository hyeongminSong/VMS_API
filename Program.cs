using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using ConsoleApp1.View;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;
namespace ConsoleApp1
{

    class TEST_Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine(await new test().Run());

            //staging
            UserData user = new UserData
            {
                Username = "hyeongmin.song",
                Password = "EKQa3WBn!4!g",
                Service_name = "VMS"
            };
            string url = "https://staging-ceo-portal-api.yogiyo.co.kr/";

            //real
            /* UserData user = new UserData
             {
                 Username = "hyeongmin.song",
                 Password = "c.X3Lrdk*SV2E!",
                 Service_name = "VMS"
             };
             string url = "https://ceo-portal-api.yogiyo.co.kr/";*/

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
                //변수 세팅(공통)
                string address = "서울특별시 동작구 서달로  123 GS25,흑석캐슬점,23층";
                string addressDetail = "";
                int managerId = 200;

                //변수 세팅(사업자)
                string companyNumber = "495-11-02764";
                string companyName = "지에스25(GS25) 흑석캐슬점";
                string companyEngName = "TEMP";
                string corpNumber = "";
                string corpName = "";
                string corpEngName = "TEMP";
                string representative = "최연호";
                string phone = "01040003416";
                string businessType = "ZZZZZ";
                string path = "C:\\Users\\A202111078\\Desktop\\픽업25-GS25범천자유점 사업자등록증.jpg.gpg";

                //변수 세팅(가게)
                string franchiseType = "franchise";
                string commissionName = "요기팩 기본 [제휴 5%]";
                string orderType = "VD";
                string franchisesName = "픽업25";

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //Console.WriteLine(await new VendorController(config).GetVendor(1001376));
                /*int vendorID = 1243716;
                string[] category_arr = { "고기/구이" };
                List<string> category_list = category_arr.ToList();
                List<category_set> getCategoryTokenList = new List<category_set>();
                //string phone = "01012345678";

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //카테고리 객체 호출 API
                JArray categoryArr = await vendorController.GetCategoryToken();
                List<JToken> targetItemList = categoryArr.Where(item => category_list.Contains((string)item["display_name"])).ToList();
                if (targetItemList.Any())
                {
                    foreach (JToken targetItem in targetItemList)
                    {
                        getCategoryTokenList.Add(new category_set()
                        {
                            id = (int)targetItem["id"],
                            name = (string)targetItem["name"],
                            category_type = (string)targetItem["category_type"]
                        });
                    }
                }

                //기등록 가게 조회 API
                JObject vendorObj = await vendorController.GetVendor(vendorID);
                //기등록 가게 -> 기등록 카테고리 Obj 저장
                JArray currnetVendorCategoryArr = (JArray)vendorObj["category_set"];
                currnetVendorCategoryArr = new JArray(vendorObj["category_set"].Where(token => token["id"] != null));
                //기등록 카테고리 Obj -> 카테고리 객체 리스트 변경
                List<category_set> categoryTokenList = currnetVendorCategoryArr.Select(j => new category_set()
                {
                    id = (int)j["id"],
                    name = (string)j["name"],
                    category_type = (string)j["category_type"],
                }).ToList();

                categoryTokenList = categoryTokenList.AsEnumerable().Union(getCategoryTokenList).ToList();
                Console.WriteLine(categoryTokenList);

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
                //Console.WriteLine(await vendorController.CheckVendorDuplicateName("API로만든가게1129182745"));

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                /*int companyID = await new CompanyController(config).GetCompanyID("333-74-33333");
                int terminationReasonID = await new TerminationVendorController(config).GetTerminationReasonID("높은 주문 실패율(킥아웃)");
                await new TerminationVendorController(config).
                    CreateTerminationVendor(companyID, "333-74-33333", 1001358, terminationReasonID, DateTime.Today.AddDays(10).ToString("yyyy-MM-dd"));*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //사업자 정보 등록 TEST
                                            
               //One ID 등록
                /*Representative representativeObj;
                JObject getOneIDObj = await companyController.GetOneIDObj("sei.kim");

                if (getOneIDObj["results"].Any())
                {
                    representativeObj = new Representative
                    {
                        id = (int)getOneIDObj["results"].First()["id"],
                        name = (string)getOneIDObj["results"].First()["name"]
                    };
                }
                else
                {
                    representativeObj = new Representative { name = representative };
                }*/


                /*JObject InquiryCompanyInfoObj = await companyController.GetInquiryCompanyInfo(companyNumber);
                string companyNumberCode = (string)InquiryCompanyInfoObj["company_number_code"];
                string companyStatus = (string)InquiryCompanyInfoObj["company_status"];
                string companyType = (string)InquiryCompanyInfoObj["company_type"];

                JObject addressInfoObj = await vendorController.GetAddressInfoObj(address);
                AddressInfo addressInfo = new GetAddressItem().GetAddressInfo(addressInfoObj, address, addressDetail);

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
                    corp_number = corpNumber,
                    corp_name_eng = corpEngName,
                    address = addressInfo,
                    //representative_set = new List<Representative> { representativeObj },
                    representative_set = new List<Representative>
                    {
                        new Representative
                        {
                            name = representative
                        }
                    },
                    corp_phone = phone,
                    //proprietor_verifi_method = proprietorVerifiMethod,
                };

                if (companyType.Contains("corp"))
                {
                    principalCompanyObj.proprietor_verifi_method = "여";
                }
                else
                {
                    principalCompanyObj.proprietor_verifi_method = "주주명부";
                }

                //비영리법인
                if (companyType == "non_profit_corp")
                {
                    principalCompanyObj.estimated_purpose = "자선";
                    principalCompanyObj.estimated_purpose_file_type = "정관";
                }

                JObject createPrincipalCompanyObj = await companyController.CreatePrincipalCompany(principalCompanyObj);

                int principalCompanyID = (int)createPrincipalCompanyObj["id"];
                JObject updatePrincipalCompanyObj = await companyController.UpdatePrincipalCompany(principalCompanyID,
                    new PrincipalCompany{ 
                    name_eng = string.Empty, 
                    corp_name_eng = string.Empty}
                    );

                Console.WriteLine(updatePrincipalCompanyObj);*/

                //Console.WriteLine(await companyController.CreateCompanyFilesObj(492, 4, path));
                //Console.WriteLine(await companyController.GetCompanyFilesObj(240, 3, path));
                //Console.WriteLine(await companyController.UpdatePrincipalCompany(241, principalCompanyObj));

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                // 가게 등록 TEST
                /* JObject updateVendorObj;
                 updateVendorObj = await vendorController.UpdateVendor(1001201,
                     new Vendor
                     {
                         order_methods = new System.Collections.Generic.List<string>() { "touch" }
                     }
                 );
                 Console.WriteLine(updateVendorObj);
                 updateVendorObj = await vendorController.UpdateVendor(1001201,
                     new Vendor
                     {
                         order_methods = new System.Collections.Generic.List<string>()
                     }
                 );
                 Console.WriteLine(updateVendorObj);
                */

                //가게 등록 개발
                JObject GetCompanyObj = await companyController.GetCompanyID(companyNumber);
                int companyID = GetCompanyObj["results"].HasValues ? (int)GetCompanyObj["results"].First()["company_set"].First()["id"] : 0;

                JObject GetFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
                int franchisesID = (int)GetFranchisesObj["results"].
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName))["id"];

                //주소 Obj
                JObject addressInfoObj = await vendorController.GetAddressInfoObj(address);
                AddressInfo addressInfo = new GetAddressItem().GetAddressInfo(addressInfoObj, address, addressDetail);

                JObject vendorObj = await vendorController.CreateVendor(new Vendor
                {
                    name = "API로만든가게" + DateTime.Now.ToString("MMddHHmmss"),
                    company = new Company
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

                //가게 생성 후 이용료/계약서 등록
                //int vendorId = 1001430;

                /*//프랜차이즈 ID 저장
                JObject getFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
                JToken franchisesTargetItem = getFranchisesObj["results"].FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));
                int franchisesID = getFranchisesObj.HasValues ? (int)franchisesTargetItem["id"] : 0;

                //정산 주체 ID 저장
                JArray vendorBillingEntityObj = await vendorController.GetVendorBillingEntities(vendorId);
                JToken billingTargetItem = vendorBillingEntityObj.
                            FirstOrDefault(item => (string)item["billing_entity_type"] == franchiseType);
                int billingTypeID = billingTargetItem.HasValues ? (int)billingTargetItem["id"] : 0;

                //주문당 이용료 ID 저장
                JArray getCommissionObj = await commissionController.GetCommissionID(true, orderType, franchisesID, commissionName);
                JToken commissionTargetItem = getCommissionObj.
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(commissionName));
                int commissionID = commissionTargetItem.HasValues ? (int)commissionTargetItem["id"] : 0;

                //주문당 이용료 등록
                CommissionContract commissionContractObj = new CommissionContract()
                {
                    vendor = vendorId,
                    commission = commissionID,
                    billing_entity_info = billingTypeID,
                    is_alliance = true,
                    franchise_type = franchiseType,
                    commission_start_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                };
                JObject createContractObj = await commissionController.AddCommissionContract(commissionContractObj);
                int createContractID = createContractObj.HasValues ? (int)createContractObj["id"] : 0;

                //계약서 생성
                ContractAudit contractAudit = new ContractAudit()
                {
                    vendor = vendorId,
                    inflow_type = "internal",
                    contract_type = "new_grand_open",
                    contract_date = DateTime.Today.ToString("yyyy-MM-dd"),
                    contract_manager = managerId,
                    is_requested_first_onboarding = true,
                    is_requested_template_menu = true,
                    commission_contract_set = new Models.Contract[] {
                        new Models.Contract{
                        id = createContractID
                    }
                },
                    zero_commission_contract_set = new Models.Contract[] { },
                    additional_fee_set = new Models.Contract[] { },
                    open_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                };
                JObject createContractAuditObj = await contractAuditController.CreateContractAudit(vendorId, contractAudit);
                int createContractAuditId = createContractAuditObj.HasValues ? (int)createContractAuditObj["id"] : 0;

                //세일즈 심사 승인 API
                Console.WriteLine(contractAuditController.RequestSalesApprove(vendorId, createContractAuditId));

                //계약서 영업 개시일 업데이트 API
                Console.WriteLine(contractAuditController.UpdateContractAudit(vendorId, createContractAuditId,
                    DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));*/

                //사장님 승인 요청 API
                /*Console.WriteLine(await contractAuditController.RequestOwnerApprove(vendorId, createContractAuditId));*/

                ////////////////////////////////////////////////////////////////////////////////////////////////////

                

                /*//티켓 첨부파일 등록
                int ticket_id = 16477;
                string filePath = "C:\\Users\\A202111078\\Desktop\\menu_excel_upload_sample_1310745.xlsx";

                JObject ticketFileObj = await vendorController.CreateTiekctFile(ticket_id, filePath);
                Console.WriteLine(ticketFileObj);
                */

                /*var vendortablePath = await new VendorDownloadController(config).
                    GetVerdorCSVFile(Path.Combine(Environment.CurrentDirectory, "vendortest.csv"));
                Console.WriteLine(vendortablePath);
                var onboardingtickettablePath = await new OnboardingTaskDownloadController(config).
                    GetTaskCSVFile(Path.Combine(Environment.CurrentDirectory, "onboardingtasktest.csv"));

                var maintainingtickettablePath = await new MaintainingTaskDownloadController(config).
                    GetTaskCSVFile(Path.Combine(Environment.CurrentDirectory, "maintainingtasktest.csv"));*/

                Console.WriteLine(config.Token);




            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            
        }

        

    }
}