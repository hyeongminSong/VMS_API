using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
                Password = "3X3tiqAXQip!",
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
                string phone = "01040003416";

                //변수 세팅(사업자)
                //string companyNumber = "495-11-02764";
                //TEST
                string companyNumber = "333-74-33333"; 

                string companyName = "지에스25(GS25) 흑석캐슬점";
                string companyEngName = "TEMP";
                string corpNumber = "";
                string corpName = "";
                string corpEngName = "TEMP";
                string representative = "최연호";
                string businessType = "ZZZZZ";
                string path = "C:\\Users\\A202111078\\Desktop\\픽업25-GS25범천자유점 사업자등록증.jpg.gpg";

                //변수 세팅(가게)

                Dictionary<string, string> verticalTypeMappings = new Dictionary<string, string>
        {
            { "음식점", "restaurant" },
            { "잡화점", "grocery" },
            { "퀵커머스", "dark_store" },
        };

                Dictionary<string, string> restaurantTypeMappings = new Dictionary<string, string>
        {
            { "0.제공하지 않음", "no_info" },
            { "1.즉석판매제조가공업", "consignment_catering_service" },
            { "2.휴게 음식점", "rest_restaurant" },
            { "3.일반음식점", "ordinary_restaurant" },
            { "4.제과점영업", "bakery" },
            { "5.기타식품판매업", "other_food_sales" },
            { "6.식육즉석판매가공업", "instant_meat_sale" },
            { "7.식품제조가공업", "food_manufacturing" },
            { "8.식육판매업", "butcher_shop" },
            { "9.기타", "etc" }
        };
                Dictionary<string, string> franchiseTypeMappings = new Dictionary<string, string>
        {
            { "직영", "direct" },
            { "가맹", "indirect" }
        };


                string vendorName = "픽업25-GS25흑석캐슬점";
                string commissionName = "오이시VD";
                string orderType = "VD";
                string franchisesName = "오이시쿠나레모에모에큥";
                string verticalType = verticalTypeMappings["잡화점"];
                string restaurantType = "";
                restaurantType = string.IsNullOrEmpty(restaurantType) ? restaurantTypeMappings.Values.First() : restaurantTypeMappings["restaurantType"];
                string licenseNumber = "";

                string deliveryDistrictComment = "최소주문금액: 0원\r\n매장기준 읍/면/동: 0원";
                ///////////////////////////////////////////////////////////
                string openingDescriptionComment = "1. 평일주말 동일 : 00:00~23:30\r\n2. 휴무일 : 연중무휴";
                string menuLeafletComment = "템플릿 메뉴 적용";

                string restaurantDeliveryMethod = "SMS+GOWIN";

                //계약 유형
                string contractType = "제휴";
                //프랜차이즈 타입
                string franchiseType = "가맹";
                franchiseType = franchiseTypeMappings[franchiseType];
                //정산 주체
                //string settlement = "그룹([HQ](주)GS리테일_GS25픽업)";
                string settlement = "프랜차이즈(오이시쿠나레모에모에큥)";
                //계약서
                string dashboardSettingText = "";
                bool isRequestedPosCode = false;
                bool isRequestedTemplateMenu = false;
                if (dashboardSettingText.Contains("POS코드 연결"))
                {
                    isRequestedPosCode = true;
                }
                if (dashboardSettingText.Contains("템플릿 메뉴 등록"))
                {
                    isRequestedTemplateMenu = true;
                }
                if (dashboardSettingText.Contains("ALL"))
                {
                    isRequestedPosCode = true;
                    isRequestedTemplateMenu = true;
                }


                //가게 부가 정보
                string appDisplayText = "Y";
                string numberDisplayText = "N";

                List<string> displayList = new List<string>();
                if (appDisplayText == "Y")
                {
                    displayList.Add("touch");
                }
                if (numberDisplayText == "Y")
                {
                    displayList.Add("phone");
                }

                string paymentMethods = "OP";
                List<string> paymentList = new List<string>();
                if(paymentMethods == "ALL")
                {
                    paymentList.Add("onsite_card");
                    paymentList.Add("onsite_cash");
                }
                bool isBelowMinimumOrderAvailable;
                string MinimumOrderAvailableText = "차액 결제 후 주문 허용";
                if(MinimumOrderAvailableText.Equals("차액 결제 후 주문 허용"))
                {
                    isBelowMinimumOrderAvailable = true;
                }
                else
                {
                    isBelowMinimumOrderAvailable = false;
                }

                int takeoutMinimumMinutes = 30;

                ////////////////////////////////////////////////////////////////////////////////////////////////////
                //카테고리
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

                /*Console.WriteLine(await vendorController.UpdateVendor(vendorID, new Vendor()
                {
                    competitor_id = "메뉴 어쩌구 CM"
                }));*/

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

                //가게 등록 TEST
                //가게 등록
                JObject vendorObj;
                int vendorID;

                JObject GetCompanyObj = await companyController.GetCompanyID(companyNumber);
                int companyID = GetCompanyObj["results"].HasValues ? (int)GetCompanyObj["results"].First()["company_set"].First()["id"] : 0;

                JObject GetFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
                int franchiseID = (int)GetFranchisesObj["results"].
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName))["id"];

                //주소 Obj
                JObject addressInfoObj = await vendorController.GetAddressInfoObj(address);
                AddressInfo addressInfo = new GetAddressItem().GetAddressInfo(addressInfoObj, address, addressDetail);

                JObject checkVendorDuplicateObj = await vendorController.CheckVendorDuplicateName(vendorName);
                bool vendorExist = (bool)checkVendorDuplicateObj["is_duplicated"];

                if (vendorExist == false)
                {
                    vendorObj = await vendorController.CreateVendor(new VendorBasicInfo
                    {
                        name = "API로만든가게" + DateTime.Now.ToString("MMddHHmmss"),
                        //name = vendorName,
                        company = new Company
                        {
                            id = companyID
                        },
                        franchise = new Franchise { id = franchiseID },
                        vendor_address = addressInfo,
                        vertical_type = verticalType,
                        business_type = restaurantType,
                        license_number = licenseNumber,
                        delivery_district_comment = deliveryDistrictComment,
                        menu_leaflet_comment = menuLeafletComment,
                        payment_methods = paymentList,
                        is_below_minimum_order_available = isBelowMinimumOrderAvailable,
                        order_methods = displayList,
                        takeout_minimum_minutes = takeoutMinimumMinutes

                    });
                    vendorID = (int)vendorObj["id"];
                }
                else
                {
                    JObject searchVendorFromNameObj = await vendorController.SearchVendorFromName(vendorName);
                    vendorID = (int)searchVendorFromNameObj["results"].First["id"];
                    vendorObj = await vendorController.UpdateVendor(vendorID, new VendorBasicInfo
                    {
                        name = "API로만든가게" + DateTime.Now.ToString("MMddHHmmss"),
                        //name = vendorName,
                        company = new Company
                        {
                            id = companyID
                        },
                        franchise = new Franchise { id = franchiseID },
                        vendor_address = addressInfo,
                        vertical_type = verticalType,
                        business_type = restaurantType,
                        license_number = licenseNumber,
                        delivery_district_comment = deliveryDistrictComment,
                        menu_leaflet_comment = menuLeafletComment,
                        payment_methods = paymentList,
                        is_below_minimum_order_available = isBelowMinimumOrderAvailable,
                        order_methods = displayList,
                        takeout_minimum_minutes = takeoutMinimumMinutes
                    });
                }

                Console.WriteLine(vendorObj);

                //가게 등록 이후 추가 등록 작업
                //int vendorID = 1001442;
                //가게 부가 정보
                vendorObj = await vendorController.UpdateVendor(vendorID, new VendorBasicInfo
                {
                    payment_methods = paymentList,
                    is_below_minimum_order_available = isBelowMinimumOrderAvailable,
                    order_methods = displayList,
                    takeout_minimum_minutes = takeoutMinimumMinutes
                });

                //영업시간 텍스트 입력
                JArray vendorDescriptionInfoObj = await vendorController.GetVendorDescriptionsInfo(vendorID);
                int openingDescriptionID = (int)vendorDescriptionInfoObj
                    .FirstOrDefault(item => (string)item["description_type"] == "opening_date")["id"];

                await vendorController.UpdateVendorDescriptionsInfo(vendorID, openingDescriptionID, openingDescriptionComment);

                //운영자 관리, 주문 전달 수단
                JObject vendorContactableEmployeesObj = await vendorController.GetVendorContactableEmployees(vendorID);
                string[] contactableEmployeesArr = vendorContactableEmployeesObj["results"]
                    .Select(token => token["phone"].ToString()).ToArray();
                if (!contactableEmployeesArr.Contains(phone))
                {
                    //운영자 추가
                    await vendorController.CreateVendorContactableEmployees(vendorID, phone);
                    //주문전달수단(SMS) 추가
                    await vendorController.UpdateVendorMobileRelayMethods(vendorID, phone);
                    //주문전달수단(GOWIN) 추가
                    if (restaurantDeliveryMethod.Contains("GOWIN"))
                    {
                        await vendorController.UpdateGowinRelayMethods(vendorID);
                    }
                }

                //가게 부가 정보 -> 주문유형 노출 여부 세팅
                JObject serviceActivationObj = await vendorController.GetServiceActivations(vendorID);
                int VDServiceActionID = (int)serviceActivationObj["results"].
                    FirstOrDefault(item => ((string)item["service_type"]).Equals("vd_status"))["id"];
                int ODServiceActionID = (int)serviceActivationObj["results"].
                    FirstOrDefault(item => ((string)item["service_type"]).Equals("od_status"))["id"];
                int TakeOutServiceActionID = (int)serviceActivationObj["results"].
                    FirstOrDefault(item => ((string)item["service_type"]).Equals("takeout_status"))["id"];

                serviceActivationObj = await vendorController.UpdateServiceActivations(vendorID, VDServiceActionID, true);
                await vendorController.UpdateServiceActivations(vendorID, ODServiceActionID, false);
                await vendorController.UpdateServiceActivations(vendorID, TakeOutServiceActionID, false);



                //가게 생성 후 이용료/계약서 등록

                //프랜차이즈 ID 저장
                JObject getFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
                JToken franchisesTargetItem = getFranchisesObj["results"].FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));
                int contractfranchiseID = getFranchisesObj.HasValues ? (int)franchisesTargetItem["id"] : 0;

                //정산 주체 ID 저장
                JArray vendorBillingEntityObj = await vendorController.GetVendorBillingEntities(vendorID);
                JToken billingTargetItem = vendorBillingEntityObj.
                            FirstOrDefault(item => (string)item["billing_entity_description"] == settlement);
                int billingTypeID = billingTargetItem.HasValues ? (int)billingTargetItem["id"] : 0;

                //주문당 이용료 ID 저장
                JArray getCommissionObj = await commissionController.GetCommissionID(true, orderType, contractfranchiseID, commissionName);
                JToken commissionTargetItem = getCommissionObj.
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(commissionName));
                int commissionID = commissionTargetItem.HasValues ? (int)commissionTargetItem["id"] : 0;

                //////////////////////////////////////////////////////////////////////////////////////////

                //주문당 이용료 등록
                CommissionContract commissionContractObj = new CommissionContract()
                {
                    vendor = vendorID,
                    commission = commissionID,
                    billing_entity_info = billingTypeID,
                    franchise_type = franchiseType,
                    commission_start_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                };
                JObject createContractObj = await commissionController.AddCommissionContract(commissionContractObj);
                int createContractID = createContractObj.HasValues ? (int)createContractObj["id"] : 0;

                //계약서 생성
                List<Models.Contract> commissionContractList = new List<Models.Contract>();
                List<Models.Contract> zeroCommissionContractList = new List<Models.Contract>();
                List<Models.Contract> additionalFeeContractList = new List<Models.Contract>();
                commissionContractList.Add(new Models.Contract
                {
                    id = createContractID,
                });
                ContractAudit contractAudit = new ContractAudit()
                {
                    vendor = vendorID,
                    contract_type = "new_grand_open",
                    contract_date = DateTime.Today.ToString("yyyy-MM-dd"),
                    contract_manager = managerId,
                    is_requested_first_onboarding = true,
                    is_requested_template_menu = isRequestedTemplateMenu,
                    is_requested_pos_code = isRequestedPosCode,
                    commission_contract_set = commissionContractList,
                    zero_commission_contract_set = zeroCommissionContractList,
                    additional_fee_set = additionalFeeContractList,
                    open_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                };
                JObject createContractAuditObj = await contractAuditController.CreateContractAudit(vendorID, contractAudit);
                int createContractAuditId = createContractAuditObj.HasValues ? (int)createContractAuditObj["id"] : 0;


                Dictionary<string, string> imageTypeMappings = new Dictionary<string, string>
        {
            { "Vendor 로고 이미지", "logo" },
            { "Vendor 큐레이션 이미지", "curation" },
            { "Vendor 뒷배경 썸네일", "background" },
            { "광고 전단지", "leaflet" },
            { "Vendor 뒷배경 이미지1", "background1" },
            { "Vendor 뒷배경 이미지2", "background2" },
            { "Vendor 뒷배경 이미지3", "background3" },
            { "Vendor 뒷배경 이미지4", "background4" },
            { "영업 신고증", "sales_registered" },
            { "배달지역 참고이미지", "delivery_district" }
        };

                Console.WriteLine(await vendorController.CreateVendorFiles(1001358, imageTypeMappings["배달지역 참고이미지"],
                    "C:\\Users\\A202111078\\Desktop\\픽업25-GS25정관달산점 영업신고증.jpg.gpg"));

                /*//세일즈 심사 승인 API
                Console.WriteLine(await contractAuditController.RequestSalesApprove(vendorID, createContractAuditId));

                //계약서 영업 개시일 업데이트 API
                Console.WriteLine(await contractAuditController.UpdateContractAudit(vendorID, createContractAuditId,
                    DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));

                //사장님 승인 요청 API
                Console.WriteLine(await contractAuditController.RequestOwnerApprove(vendorID, createContractAuditId));*/

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