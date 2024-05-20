using ConsoleApp1.Controller;
using ConsoleApp1.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
namespace ConsoleApp1
{

    class TEST_Program
    {
        static async Task Main(string[] args)
        {
            //Console.WriteLine(await new test().Run());

            //staging
            UserData VMSuser = new UserData
            {
                Username = "hyeongmin.song",
                Password = "@r8G!RNzjBhG",
                Service_name = "VMS"
            };
            string url = "https://staging-ceo-portal-api.yogiyo.co.kr/";

            //real
            /*UserData VMSuser = new UserData
            {
                Username = "hyeongmin.song",
                Password = "wRtqQw5cTraMDfq!",
                Service_name = "VMS"
            };
            string url = "https://ceo-portal-api.yogiyo.co.kr/";*/

            Config config = new Config(url);
            config = await new TokenController(config, VMSuser).GetAccessToken();

            CompanyController companyController = new CompanyController(config);
            FranchisesController franchisesController = new FranchisesController(config);
            VendorController vendorController = new VendorController(config);
            CommissionController commissionController = new CommissionController(config);
            ContractAuditController contractAuditController = new ContractAuditController(config);
            TicketController ticketController = new TicketController(config);
            CompanyAssetController companyAssetController = new CompanyAssetController(config);
            ScheduleContoller scheduleController = new ScheduleContoller(config);
            UserController userController = new UserController(config);
            TerminationVendorController terminationVendorController = new TerminationVendorController(config);


            try
            {
                Console.WriteLine("Bearer " + config.Token);
                //변수 세팅(공통)
                string address = "전라남도 목포시 영산로  719-32";
                string addressDetail = "";
                int managerId = 200;
                string phone = "01092688421";

                //변수 세팅(사업자)
                //string companyNumber = "495-11-02764";
                //TEST
                string companyNumber = "189-04-03142";

                string companyName = "씨유 목포한양점";
                string companyEngName = "TEMP";
                string corpNumber = "";
                string corpName = "";
                string corpEngName = "TEMP";
                string representative = "송형민";
                string businessType = "ZZZZZ";
                //string path = "C:\\Users\\A202111078\\Desktop\\픽업25-GS25범천자유점 사업자등록증.jpg.gpg";

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


                string vendorName = "이마트24-진해중원로터리점";
                string commissionName = "오이시VD";
                string orderType = "VD";
                string franchisesName = "이마트24";
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
                if (paymentMethods == "ALL")
                {
                    paymentList.Add("onsite_card");
                    paymentList.Add("onsite_cash");
                }
                else
                {
                    paymentList = null;
                }
                bool isBelowMinimumOrderAvailable;
                string MinimumOrderAvailableText = "차액 결제 후 주문 허용";
                if (MinimumOrderAvailableText.Equals("차액 결제 후 주문 허용"))
                {
                    isBelowMinimumOrderAvailable = true;
                }
                else
                {
                    isBelowMinimumOrderAvailable = false;
                }

                int takeoutMinimumMinutes = 30;

                ////////////////////////////////////////////////////////
                ///
                int company_id = 131475;
                string company_address = "경기도 용인시 수지구 이현로 107 한일코지세상";
                string company_address_detail = "2동 101호, 201호";
                string company_representative = "최지혜";
                string company_brand_kr = "지에스25수지이현";
                string company_phone = "01020300813";
                string corp_number = "";
                string corp_name = "";

                ////////////////////////////////////////////////////////
                ///계약서

                /*var vendor_id = 1001448;
                var order_type = orderType;
                var franchise_type = franchiseType;
                var franchise_name = "오이시쿠나레모에모에큥";
                var commission_name = commissionName;

                *//*JArray commissionArr = await commissionController.GetCommission("", null, "");

                List<Commission> getCommissionList = JsonConvert.DeserializeObject<List<Commission>>(commissionArr.ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });*//*


                FranchiseReadOnly franchise = null;
                BillingEntities billingEntity = null;
                Commission commission = null;

                if (franchise_type != null)
                {
                    franchise_type = franchiseType;
                }
                else
                {
                    franchise_type = "none";
                }

                if (franchise_name != null)
                {
                    JObject getFranchisesObj = await franchisesController.GetFranchisesID(false, franchise_name);
                    if (getFranchisesObj == null || getFranchisesObj["results"].HasValues == false)
                    {
                        new Exception("프랜차이즈 조회 실패");
                        return;
                    }
                    FranchiseReadOnly[] franchises = JsonConvert.DeserializeObject<FranchiseReadOnly[]>(getFranchisesObj["results"].ToString(),
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore
                        });
                    franchise = franchises.FirstOrDefault(item => item.name.EndsWith(franchise_name));
                }

                //정산 주체 ID 저장
                JArray billingEntityArr = await vendorController.GetVendorBillingEntities(vendor_id);
                if (billingEntityArr == null || billingEntityArr.HasValues == false)
                {
                    new Exception("정산 주체 조회 실패");
                    return;
                }
                BillingEntities[] billingEntities = JsonConvert.DeserializeObject<BillingEntities[]>(billingEntityArr.ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });

                billingEntity = billingEntities.FirstOrDefault(item => item.billing_entity_description.Equals(settlement));

                if (billingEntity != null)
                {
                    int franchise_id = franchise == null ? 0 : franchise.id;

                    //주문당 이용료 ID 저장
                    JArray commissionArr = await commissionController.GetCommission(order_type, franchise_id, commission_name);
                    if (commissionArr == null || commissionArr.HasValues == false)
                    {
                        new Exception("주문당 이용료 조회 실패");
                        return;
                    }
                    List<Commission> getCommissionList = JsonConvert.DeserializeObject<List<Commission>>(commissionArr.ToString(),
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore
                        });

                    commission = getCommissionList.FirstOrDefault(item => item.name.EndsWith(commission_name));
                }

                if (commission != null)
                {
                    CommissionContract commissionContractObj = new CommissionContract()
                    {
                        vendor = vendor_id,
                        commission = commission.id,
                        billing_entity_info = billingEntity.id,
                        franchise_type = franchise_type,
                        commission_start_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                    };
                    JObject createContractObj = await commissionController.AddCommissionContract(commissionContractObj);
                    if (createContractObj == null || createContractObj.HasValues == false)
                    {
                        new Exception("이용료계약정보 생성 실패");
                        return;
                    }
                    CommissionContract commissionContract = JsonConvert.DeserializeObject<CommissionContract>(createContractObj.ToString(),
                        new JsonSerializerSettings
                        {
                            NullValueHandling = NullValueHandling.Ignore,
                            DefaultValueHandling = DefaultValueHandling.Ignore
                        });
                    Console.WriteLine(commissionContract);
                }*/

                /*if (contractfranchiseID != 0)
                {
                    //정산 주체 ID 저장
                    JArray vendorBillingEntityObj = await vendorController.GetVendorBillingEntities(vendor_id);
                    if (vendorBillingEntityObj == null || vendorBillingEntityObj["results"].HasValues == false)
                    {

                    }
                    else
                    {
                        JToken billingTargetItem = vendorBillingEntityObj.
                            FirstOrDefault(item => (string)item["billing_entity_description"] == settlement);
                        if (billingTargetItem != null)
                        {
                            billingTypeID = (int)billingTargetItem["id"];
                        }
                    }
                }

                if (billingTypeID != 0)
                {
                    //주문당 이용료 ID 저장
                    JArray getCommissionObj = await commissionController.GetCommission(order_type, contractfranchiseID, commission_name);
                    JToken commissionTargetItem = getCommissionObj.
                                FirstOrDefault(item => ((string)item["name"]).EndsWith(commission_name));
                    if (commissionTargetItem != null)
                    {
                        commission_id = (int)commissionTargetItem["id"];
                    }
                }

                if (commission_id != 0)
                {
                    CommissionContract commissionContractObj = new CommissionContract()
                    {
                        vendor = vendor_id,
                        commission = commission_id,
                        billing_entity_info = billingTypeID,
                        franchise_type = franchiseTypeMappings[franchise_type],
                        commission_start_date = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")
                    };
                    JObject createContractObj = await commissionController.AddCommissionContract(commissionContractObj);
                }*/
                var vendor_id = 1001448;
                var order_type = orderType;
                var franchise_type = franchiseType;
                var franchise_name = "오이시쿠나레모에모에큥";
                var commission_name = commissionName;
                int commission_amount = 79900;

                ZeroCommission zeroCommission = null;
                
                //월정액 이용료 ID 저장
                JArray zeroCommissionArr = await commissionController.GetZeroCommission();
                if (zeroCommissionArr == null || zeroCommissionArr.HasValues == false)
                {
                    new Exception("월정액 이용료 조회 실패");
                    return;
                }
                ZeroCommission[] getZeroCommissionArr = JsonConvert.DeserializeObject<ZeroCommission[]>(zeroCommissionArr.ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });

                zeroCommission = getZeroCommissionArr.FirstOrDefault(item => item.commission_amount == commission_amount);

                if (zeroCommission == null)
                {
                    new Exception("월정액 이용료 조회 실패");
                    return;
                }

                ///월정액 이용료 등록 조건
                ///1. 카테고리가 등록되어 있어야함
                ///Category -> category = new category(){id = getCategory.id} 객체 생성
                ///2. 배달지역이 등록되어 있어야함
                ///2.1 - vendorController.GetAddressInfoObj(배달지역) -> id 획득
                ///AdminArea -> admin_area = new AdminArea() { id = getAddressInfo.id} 객체 생성

                ZeroCommissionContract zeroCommissionContract = new ZeroCommissionContract()
                {
                    vendor = vendor_id,
                    zero_commission = zeroCommission,
                    commission_start_date = DateTime.Today.ToString("yyyy-MM-dd"),
                };
                JObject zeroCommissionContractObj = await commissionController.AddZeroCommissionContract(zeroCommissionContract);
            



                ///
            /*                JObject contractAuditObj = await contractAuditController.GetContractAudit(1001172);

                            //JToken contractAuditToken = contractAuditObj["results"].OrderByDescending(item => (DateTime)item["contract_date"]).FirstOrDefault();
                            JToken contractAuditToken = contractAuditObj["results"].OrderByDescending(item => (DateTime)item["contract_date"]).Last();

                            List<JToken> commissionContractObj = contractAuditToken["commission_contract_set"].Select(commission_contract => commission_contract["commission"]).ToList();
                            bool vd_check = commissionContractObj.Where(contract => contract["order_type"].ToString().Equals("VD")).Any();
                            bool od_check = commissionContractObj.Where(contract => contract["order_type"].ToString().Equals("OD")).Any();
                            bool takeout_check = commissionContractObj.Where(contract => contract["order_type"].ToString().Equals("TAKEOUT")).Any();
                            Console.WriteLine(commissionContractObj);
            */
            /*JObject currentCompanyObj = await companyController.GetPrincipalCompanyFromID(company_id);
            if (currentCompanyObj == null || currentCompanyObj.HasValues == false)
            {
                Console.WriteLine("사업자 정보 조회 실패");
            }
            string company_number = (string)currentCompanyObj["company_number"];

            JObject InquiryCompanyInfoObj = await companyController.GetInquiryCompanyInfo(company_number);
            if (InquiryCompanyInfoObj == null || string.IsNullOrEmpty(InquiryCompanyInfoObj["error"].ToString()) == false)
            {
                Console.WriteLine("사업자 번호 조회 실패");
            }

            string companyNumberCode = (string)InquiryCompanyInfoObj["company_number_code"];
            string companyStatus = (string)InquiryCompanyInfoObj["company_status"];
            string companyType = (string)InquiryCompanyInfoObj["company_type"];

            JObject addressInfoObj = await vendorController.GetAddressInfoObj(company_address);
            if (addressInfoObj == null || addressInfoObj["items"].HasValues == false)
            {
                Console.WriteLine("주소 조회 실패");
            }
            AddressInfo addressInfo = new GetAddressItem().GetAddressInfo(addressInfoObj, company_address, company_address_detail);

            PrincipalCompany principalCompanyObj = JsonConvert.DeserializeObject<PrincipalCompany>(currentCompanyObj.ToString(), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });
            if (principalCompanyObj.representative_set.Select(item => item.name).Contains(company_representative))
            {
                principalCompanyObj.representative_set = null;
            }
            else
            {
                principalCompanyObj.representative_set = principalCompanyObj.representative_set.
                    Union(new Representative[] { new Representative { name = company_representative } }).ToArray();
            }

            principalCompanyObj.company_number = company_number;
            principalCompanyObj.company_number_code = companyNumberCode;
            principalCompanyObj.company_status = companyStatus;
            principalCompanyObj.business_type = "ZZZZZ";
            principalCompanyObj.name = company_brand_kr;
            principalCompanyObj.name_eng = string.Empty;
            principalCompanyObj.address = addressInfo;
            principalCompanyObj.corp_number = corp_number;
            principalCompanyObj.corp_name = corp_name;
            principalCompanyObj.corp_name_eng = string.Empty;
            principalCompanyObj.corp_phone = company_phone;


            if (companyType.Contains("corp"))
            {
                principalCompanyObj.proprietor_verifi_method = "주주명부";

                //비영리법인
                if (companyType == "non_profit_corp")
                {
                    principalCompanyObj.estimated_purpose = "자선";
                    principalCompanyObj.estimated_purpose_file_type = "정관";
                }
            }
            else
            {
                principalCompanyObj.proprietor_verifi_method = "여";
            }*/

            //TASK 유형
            /*JObject ticketTypeObj = await ticketController.GetTicketType("onboarding_yogiyo", "메뉴/배달지역 검수");
            if (ticketTypeObj["results"].HasValues == false)
            {
                Console.WriteLine("TASK 유형 조회 실패");
                return;
            }
            TicketType currentTicketType = JsonConvert.DeserializeObject<TicketType>(ticketTypeObj["results"].First().ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });*/


            /*JObject updatePrincipalCompanyObj = await companyController.UpdatePrincipalCompany(company_id, principalCompanyObj);
            if (updatePrincipalCompanyObj == null || updatePrincipalCompanyObj.HasValues == false)
            {
                Console.WriteLine("사업자 정보 업데이트 실패");
                return;
            }
            Console.WriteLine((int)updatePrincipalCompanyObj["id"]);*/

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
            ///
            /*companyNumber = "333-74-33333";
            int vendorID = 1001201;
            string terminationReasonText = "높은 주문 실패율(킥아웃)";

            JObject getVendorObj = await vendorController.GetVendor(1001201);
            if (getVendorObj == null || getVendorObj.HasValues == false)
            {
                Console.WriteLine("가게 조회 실패");
            }
            VendorBasicInfo currentVendorBasicInfo = JsonConvert.DeserializeObject<VendorBasicInfo>(getVendorObj.ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            int companyID = currentVendorBasicInfo.company.id;

            JObject terminationReasonObj = await terminationVendorController.GetTerminationReasonID();
            if (terminationReasonObj == null || terminationReasonObj["results"].HasValues == false)
            {
                Console.WriteLine("계약 해지 사유 조회 실패");
            }
            TerminationReasonSet[] terminationReasonSet = JsonConvert.DeserializeObject<TerminationReasonSet[]>(terminationReasonObj["results"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            int terminationReasonID = terminationReasonSet.FirstOrDefault(terminationReason => terminationReason.reason.Contains(terminationReasonText)).id;

            BulkCreateTerminationVendor bulkCreateTerminationVendor = new BulkCreateTerminationVendor()
            {
                company_id = companyID,
                company_number = companyNumber,
                vendor_ids = new int[] { vendorID },
                termination = new termination()
                {
                    reason = terminationReasonID,
                    termination_date = DateTime.Today.AddDays(10).ToString("yyyy-MM-dd")
                }
            };

            JArray createTerminationArr = await terminationVendorController.CreateTerminationVendor(bulkCreateTerminationVendor);

            if (createTerminationArr == null || createTerminationArr.HasValues == false)
            {
                Console.WriteLine("계약 해지 실패");
            }*/

            ////////////////////////////////////////////////////////////////////////////////////////////////////
            //사업자 정보 등록 TEST

            /*JObject companyObj = await companyController.GetCompany(companyNumber);
            if (companyObj != null || companyObj["results"].HasValues == false)
            {
                Models.Company getCompanyObj = JsonConvert.DeserializeObject<Models.Company>(companyObj["results"].First().ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }*/

            /*JObject InquiryCompanyInfoObj = await companyController.GetInquiryCompanyInfo(companyNumber);
            string companyNumberCode = (string)InquiryCompanyInfoObj["company_number_code"];
            string companyStatus = (string)InquiryCompanyInfoObj["company_status"];
            string companyType = (string)InquiryCompanyInfoObj["company_type"];

            JObject addressInfoObj = await vendorController.GetAddressInfoObj(address);
            AddressInfo addressInfo = new GetAddressItem().GetAddressInfo(addressInfoObj, address, addressDetail);

            //Console.WriteLine(await companyController.GetCompanyID(companyNumber));
            Representative representativeObj = new Representative { name = representative, is_active = true, };

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
                representative_set = new Representative[] { representativeObj },
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
                new PrincipalCompany
                {
                    name_eng = string.Empty,
                    corp_name_eng = string.Empty
                }
                );

            Console.WriteLine(updatePrincipalCompanyObj);*/

            /*JObject InquiryCompanyInfoObj = await companyController.GetInquiryCompanyInfo("317-65-00590");
            if (InquiryCompanyInfoObj == null || string.IsNullOrEmpty(InquiryCompanyInfoObj["error"].ToString()) == false)
            {
                Console.WriteLine("사업자 번호 조회 실패");
                return;
            }

            string companyStatus = (string)InquiryCompanyInfoObj["company_status"];
            string companyType = (string)InquiryCompanyInfoObj["company_type"];

            Console.WriteLine(companyStatus.Contains("정상"));
            Console.WriteLine(companyType.Contains("corp"));*/

            //One ID 등록
            /*JObject getObj = await companyController.GetPrincipalCompanyFromID(236635);
            Console.WriteLine(getObj);
            PrincipalCompany principalCompanyObj = JsonConvert.DeserializeObject<PrincipalCompany>(getObj.ToString());

            string oneID = "hyeongmin.song";
            Models.사업자.User user;
            JObject userObj = await userController.GetUser(oneID, "", "");
            if (userObj != null || userObj["results"].HasValues == false)
            {
                user = JsonConvert.DeserializeObject<Models.사업자.User>(userObj["results"].First().ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }
            else
            {
                user = null;
            }

            JObject getCompanyObj = await companyController.GetCompany(companyNumber);
            int companyID;
            if (getCompanyObj != null || getCompanyObj["results"].HasValues ||
                getCompanyObj["results"].AsEnumerable().Where(item => item["id"].Equals(principalCompanyObj.id)).Any())
            {
                companyID = (int)getCompanyObj["results"].AsEnumerable().
                    Where(item => Convert.ToInt32(item["id"]).Equals(principalCompanyObj.id)).First()["company_set"].First()["id"];
            }
            else
            {
                companyID = 0;
                Console.WriteLine("실패");
            }

            //PrincipalCompany principalCompany = JsonConvert.DeserializeObject<PrincipalCompany>(updatePrincipalCompanyObj.ToString());

            Models.Company companyObj = new Models.Company()
            {
                name = user.name,
                is_active = true,
                subordinate_num = string.Empty,
                principal_company = new PrincipalCompany()
                {
                    id = principalCompanyObj.id,
                },
                user = new Models.사업자.User()
                {
                    id = user.id,
                },
            };

            *//*JObject createCompanyObj = await companyController.CreateCompany(companyObj);
            Console.WriteLine(createCompanyObj);*//*

            // JObject updatePrincipalCompanyObj = await companyController.UpdatePrincipalCompany(236634, principalCompany);
            JObject updateCompanyObj = await companyController.UpdateCompany(companyID, companyObj);
            Console.WriteLine(updateCompanyObj);*/

            //Console.WriteLine(await companyController.CreateCompanyFilesObj(492, 4, path));
            //Console.WriteLine(await companyController.GetCompanyFilesObj(240, 3, path));
            //Console.WriteLine(await companyController.UpdatePrincipalCompany(241, principalCompanyObj));

            ////////////////////////////////////////////////////////////////////////////////////////////////////

            //가게 등록 TEST
            /*//가게 등록
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
                    //name = "API로만든가게" + DateTime.Now.ToString("MMddHHmmss"),
                    name = vendorName,
                    company = new Models.Company
                    {
                        id = companyID
                    },
                    franchise = new Franchise { id = franchiseID },
                    vendor_address = addressInfo,
                    vertical_type = verticalType,
                    business_type = restaurantType,
                    license_number = licenseNumber,
                    *//*delivery_district_comment = deliveryDistrictComment,
                    menu_leaflet_comment = menuLeafletComment,
                    payment_methods = paymentList,
                    is_below_minimum_order_available = isBelowMinimumOrderAvailable,
                    order_methods = displayList,
                    takeout_minimum_minutes = takeoutMinimumMinutes*//*

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
                    company = new Models.Company
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

            Console.WriteLine(vendorObj);*/

            //가게 등록 이후 추가 등록 작업
            /*vendorID = 1001423;
            //가게 부가 정보
            *//*vendorObj = await vendorController.UpdateVendor(vendorID, new VendorBasicInfo
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
            }*//*

            //가게 부가 정보 -> 주문유형 노출 여부 세팅
            JObject serviceActivationObj = await vendorController.GetServiceActivations(vendorID);
            int VDServiceActionID = (int)serviceActivationObj["results"].
                FirstOrDefault(item => ((string)item["service_type"]).Equals("vd_status"))["id"];
            int ODServiceActionID = (int)serviceActivationObj["results"].
                FirstOrDefault(item => ((string)item["service_type"]).Equals("od_status"))["id"];
            int TakeOutServiceActionID = (int)serviceActivationObj["results"].
                FirstOrDefault(item => ((string)item["service_type"]).Equals("takeout_status"))["id"];

            serviceActivationObj = await vendorController.UpdateServiceActivations(vendorID, TakeOutServiceActionID, true);




            //가게 생성 후 이용료/계약서 등록
            int contractfranchiseID = 0;
            int billingTypeID = 0;

            //프랜차이즈 ID 저장
            JObject getFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
            if (getFranchisesObj["results"].HasValues)
            {
                JToken franchisesTargetItem = getFranchisesObj["results"].FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));
                contractfranchiseID = (int)franchisesTargetItem["id"];
            }
            *//*JToken franchisesTargetItem = getFranchisesObj["results"].FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName));
            int contractfranchiseID = getFranchisesObj["results"].HasValues ? (int)franchisesTargetItem["id"] : 0;*//*

            //정산 주체 ID 저장
            JArray vendorBillingEntityObj = await vendorController.GetVendorBillingEntities(vendorID);
            JToken billingTargetItem = vendorBillingEntityObj.
                        FirstOrDefault(item => (string)item["billing_entity_description"] == settlement);
            if(billingTargetItem != null)
            {
                billingTypeID = billingTargetItem.HasValues ? (int)billingTargetItem["id"] : 0;
            }
            //int billingTypeID = billingTargetItem.HasValues ? (int)billingTargetItem["id"] : 0;

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
            List<Contract> commissionContractList = new List<Contract>();
            List<Contract> zeroCommissionContractList = new List<Contract>();
            List<Contract> additionalFeeContractList = new List<Contract>();
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
            int createContractAuditId = createContractAuditObj.HasValues ? (int)createContractAuditObj["id"] : 0;*/


            /*Dictionary<string, string> imageTypeMappings = new Dictionary<string, string>
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
*/
            /*//세일즈 심사 승인 API
            Console.WriteLine(await contractAuditController.RequestSalesApprove(vendorID, createContractAuditId));

            //계약서 영업 개시일 업데이트 API
            Console.WriteLine(await contractAuditController.UpdateContractAudit(vendorID, createContractAuditId,
                DateTime.Today.AddDays(1).ToString("yyyy-MM-dd")));

            //사장님 승인 요청 API
            Console.WriteLine(await contractAuditController.RequestOwnerApprove(vendorID, createContractAuditId));*/

            //가게 복사
            /*companyNumber = "343-05-02991";
            franchisesName = "GSTHEFRESH";
            verticalType = "잡화점";*/

            /*JObject getVendorObj = await vendorController.GetVendor(1322955);
            VendorBasicInfo currentVendorBasicInfo = JsonConvert.DeserializeObject<VendorBasicInfo>(getVendorObj.ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            JObject getetContactableEmployeesObj = await vendorController.GetContactableEmployees(1322955);
            if (getetContactableEmployeesObj["results"].HasValues)
            {
                ContactableEmployee[] contactableEmployeeArr = JsonConvert.DeserializeObject<ContactableEmployee[]>(getetContactableEmployeesObj["results"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            }


            //사업자번호
            JObject GetCompanyObj = await companyController.GetCompanyID(companyNumber);
            if (GetCompanyObj["results"].HasValues == false)
            {
                new Exception("사업자번호 조회 실패");
                return;
            }
            int companyID = (int)GetCompanyObj["results"].First()["company_set"].First()["id"];

            Models.Company company = new Models.Company()
            {
                id = companyID,
            };

            //프랜차이즈
            Franchise franchise = new Franchise();

            if (string.IsNullOrEmpty(franchisesName) == false)
            {
                JObject GetFranchisesObj = await franchisesController.GetFranchisesID(false, franchisesName);
                if (GetFranchisesObj["results"].HasValues == false)
                {
                    new Exception("프랜차이즈 조회 실패");
                    return;
                }

                int franchiseID = (int)GetFranchisesObj["results"].
                            FirstOrDefault(item => ((string)item["name"]).EndsWith(franchisesName))["id"];
                franchise = new Franchise()
                {
                    id = franchiseID,
                };
            }

            currentVendorBasicInfo.name = "API로복사해서만든가게" + DateTime.Now.ToString("MMddHHmmss");
            currentVendorBasicInfo.company = company;
            currentVendorBasicInfo.franchise = franchise;
            currentVendorBasicInfo.vertical_type = verticalTypeMappings[verticalType];*/

            /*JObject copyVendorObj = await vendorController.CreateVendor(currentVendorBasicInfo);
            if (copyVendorObj == null || copyVendorObj.HasValues == false)
            {
                new Exception("가게 복사 실패");
                return;
            }
            Console.WriteLine(copyVendorObj);*/


            /*JToken addressTargetItem = getVendorObj["vendor_address"];
            AddressInfo addressInfo = new AddressInfo
            {
                lat = (double)addressTargetItem["lat"],
                lon = (double)addressTargetItem["lon"],
                zip_code = addressTargetItem["zipcode"].ToString() ?? string.Empty,
                sido = addressTargetItem["sido"].ToString() ?? string.Empty,
                sigugun = addressTargetItem["sigugun"].ToString() ?? string.Empty,
                admin_dongmyun = addressTargetItem["admin_dongmyun"].ToString() ?? string.Empty,
                law_dongmyun = addressTargetItem["law_dongmyun"].ToString() ?? string.Empty,
                road_dongmyun = addressTargetItem["road_dongmyun"].ToString() ?? string.Empty,
                ri = addressTargetItem["ri"].ToString() ?? string.Empty,
                admin_detailed_address = addressTargetItem["admin_detailed_address"].ToString() ?? string.Empty,
                law_detailed_address = addressTargetItem["law_detailed_address"].ToString() ?? string.Empty,
                road_detailed_address = addressTargetItem["road_detailed_address"].ToString() ?? string.Empty,
                custom_detailed_address = addressTargetItem["custom_detailed_address"].ToString() ?? string.Empty
            };*/


            ////////////////////////////////////////////////////////////////////////////////////////////////////



            /*//티켓 첨부파일 등록
            int ticket_id = 16477;
            string filePath = "C:\\Users\\A202111078\\Desktop\\menu_excel_upload_sample_1310745.xlsx";

            JObject ticketFileObj = await vendorController.CreateTiekctFile(ticket_id, filePath);
            Console.WriteLine(ticketFileObj);
            */
            /*string filePath = "C:\\Users\\A202111078\\Desktop\\TEST.csv";

            await ticketController.GetTaskCSVFile(filePath, "Onboarding");
            if (File.Exists(filePath))
            {
                Console.WriteLine("성공");
            }
            else
            {
                throw new Exception("온보딩 파일 다운로드 실패");
            }*/
            /*JObject ticketInfoObj = await ticketController.GetTicketInfomation(99999999);
            Console.WriteLine(ticketInfoObj.HasValues);*/

            /*JObject getVendorObj = await vendorController.GetVendor(1001422);
            Console.WriteLine(getVendorObj);
            VendorBasicInfo newVendor = JsonConvert.DeserializeObject<VendorBasicInfo>(getVendorObj.ToString(), new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Ignore
            });*/

            /*List<string> category_list = new List<string>() { "야식" };

            List<category_set> categoryTokenList = new List<category_set>();
            if (category_list != null)
            {
                List<category_set> getCategoryTokenList = new List<category_set>();
                //카테고리 객체 호출 API
                JArray categoryArr = await vendorController.GetCategoryToken();
                List<JToken> targetItemList = categoryArr.Where(item => category_list.Contains((string)item["display_name"])).ToList();
                List<category_set> targetCategoryList = targetItemList.Select(item => item.ToObject<category_set>()).ToList();

                //기등록 카테고리 Obj -> 카테고리 객체 리스트 변경
                categoryTokenList = newVendor.category_set.AsEnumerable().Union(targetCategoryList).ToList();
            }

            List<string> displayListTemp = newVendor.order_methods;

            Console.WriteLine(displayListTemp);

            VendorBasicInfo createVendor = new VendorBasicInfo
            {
                name = newVendor.name,
                company = newVendor.company,
                franchise = newVendor.franchise,
                vendor_address = newVendor.vendor_address,
                vertical_type = newVendor.vertical_type,
                business_type = newVendor.business_type,
                license_number = newVendor.license_number,
                category_set = newVendor.category_set,
                order_methods = newVendor.order_methods,
            };
            JObject vendorObj = await vendorController.CreateVendor(createVendor);
            Console.WriteLine(vendorObj);*/


            /*JObject vendorObj = await vendorController.UpdateVendor(1001422, new VendorBasicInfo
            {
                competitor_id = "TEST",
            });*/


            /*int task_id = 4130595;
            TicketStatus task_status = TicketStatus.complete;
            string type_depth1 = "onboarding_yogiyo";
            string type_depth2 = "계약 심사";
            string task_organization_depth1 = "CXI 본부";
            string task_organization_depth2 = "CX 기획실";
            string task_organization_depth3 = null;
            if (string.IsNullOrEmpty(task_organization_depth3))
            {
                task_organization_depth3 = string.Empty;
            }
            string task_title = null;
            string task_description = null;
            string task_assignee = "박희진";
            string task_rejected_reason = "";

            JObject organizationObj = await ticketController.GetOrganization(task_organization_depth1, task_organization_depth2, task_organization_depth3);
            JObject managerObj = await companyAssetController.GetStaff(task_assignee, task_organization_depth1, task_organization_depth2, task_organization_depth3);

            //티켓 생성

            //TASK 유형
            JObject ticketTypeObj = await ticketController.GetTicketType(type_depth1, type_depth2);
            if (ticketTypeObj["results"].HasValues == false)
            {
                new Exception("TASK 유형 조회 실패");
            }
            TicketType currentTicketType = JsonConvert.DeserializeObject<TicketType>(ticketTypeObj["results"].First().ToString());

            // 가게 기본 정보 조회
            JObject getVendorObj = await vendorController.GetVendor(1001440);
            TargetVendor targetVendor = new TargetVendor()
            {
                id = (int)getVendorObj["id"],
            };
            NestedCompanyRelatedField targetCompany = new NestedCompanyRelatedField()
            {
                id = (int)getVendorObj["company"]["id"],
            };

            TargetUser targetUser;
            if (getVendorObj["company"]["user"].HasValues)
            {
                targetUser = new TargetUser()
                {
                    id = (int)getVendorObj["company"]["user"]["id"]
                };

            }
            else
            {
                targetUser = null;
            }

            // 조직 조회
            //JObject organizationObj = await ticketController.GetOrganization(task_organization_depth1, task_organization_depth2, task_organization_depth3);

            AssignedOrganization assignedOrganization;
            if (organizationObj["results"].HasValues)
            {
                assignedOrganization = new AssignedOrganization()
                {
                    depth1 = (string)organizationObj["results"].First()["depth1"],
                    depth2 = (string)organizationObj["results"].First()["depth2"],
                    depth3 = (string)organizationObj["results"].First()["depth3"],
                    id = (int)organizationObj["results"].First()["id"],
                };
            }
            else
            {
                assignedOrganization = new AssignedOrganization()
                {
                    id = currentTicketType.default_assigned_organization.id
                };
            }

            Ticket ticket = new Ticket()
            {
                status = TicketStatus.ready.ToString(),
                title = task_title,
                description = task_description,
                ticket_type = currentTicketType,
                assigned_organization = assignedOrganization,
                target_vendor = targetVendor,
                target_company = targetCompany,
                target_user = targetUser,
            };

            JObject createTicketObj = await ticketController.CreateTicket(ticket);
            if (createTicketObj.HasValues == false)
            {
                new Exception("티켓 생성 실패");
            }*/

            //티켓 조회
            /*JObject searchTicketObj = await ticketController.SearchTicket(1001447, "ready", type_depth1, type_depth2);
            if (searchTicketObj == null || searchTicketObj["results"].HasValues == false)
            {
                new Exception("티켓 조회 실패");
            }
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(searchTicketObj["results"].First().ToString());
            task_id = ticket.id;

            //티켓 업데이트
            JObject currentTicketObj = await ticketController.GetTicketInfomation(task_id);
            Ticket currentTicket = JsonConvert.DeserializeObject<Ticket>(currentTicketObj.ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            //이관 할 부서
            AssignedOrganization assignedOrganization = null;

            JObject organizationObj = await ticketController.GetOrganization(task_organization_depth1, task_organization_depth2, task_organization_depth3);
            if (organizationObj["results"].HasValues)
            {
                assignedOrganization = new AssignedOrganization()
                {
                    depth1 = (string)organizationObj["results"].First()["depth1"],
                    depth2 = (string)organizationObj["results"].First()["depth2"],
                    depth3 = (string)organizationObj["results"].First()["depth3"],
                    id = (int)organizationObj["results"].First()["id"],
                };
            }
            else
            {
                new Exception("이관 할 부서 조회 실패");
            }

            if (task_status.ToString().Equals("complete"))
            {
                currentTicket.is_success = true;
                if (task_rejected_reason != null) // 티켓 타입에 입력한 보류 사유 있는 지 체크
                {
                    if (currentTicket.ticket_type.rejected_reasons.Contains(task_rejected_reason))
                    {
                        currentTicket.is_success = false;
                        currentTicket.rejected_reason = task_rejected_reason;
                    }
                }
            }

            currentTicket.status = task_status.ToString();
            currentTicket.assigned_organization = assignedOrganization;
            currentTicket.title = task_title;
            currentTicket.description = task_description;

            JObject updateTicketObj = await ticketController.UpdateTicket(task_id, currentTicket);
            Console.WriteLine(updateTicketObj);
            if (updateTicketObj.HasValues == false)
            {
                new Exception("티켓 업데이트 실패");
            }

            if (string.IsNullOrEmpty(task_assignee) == false)
            {
                //처리자
                int managerID = 0;

                if (task_assignee != null) // 처리자 있는 경우
                {
                    JObject managerObj = await companyAssetController.GetStaff(task_assignee, assignedOrganization.depth1, assignedOrganization.depth2, assignedOrganization.depth3);
                    if (managerObj["results"].HasValues)
                    {
                        managerID = (int)managerObj["results"].First()["id"];
                    }
                    else
                    {
                        new Exception("처리자 확인 실패");
                    }
                }
                JObject updateTicketAssigneeObj = await ticketController.UpdateTicketAssignee(task_id, new TicketAssignee() { assignee = new Assignee() { id = managerID } });
                if (updateTicketAssigneeObj.HasValues == false)
                {
                    new Exception("처리자 지정 실패");
                }
            }*/

            /*//정기 휴무일 생성
            List<holiday_schedule_meta> holidayScheduleMetaList = new List<holiday_schedule_meta>();

            // DataTable 생성
            DataTable table = new DataTable();

            // 열 추가
            table.Columns.Add("weekday", typeof(string));
            table.Columns.Add("holiday_period", typeof(int));

            // 데이터 추가
            table.Rows.Add("Mon", -1);
            table.Rows.Add("Sun", 1);
            table.Rows.Add("Sun", 3);

            foreach (var row in table.AsEnumerable())
            {
                holidayScheduleMetaList.Add(new holiday_schedule_meta()
                {
                    holiday_period = Convert.ToInt32(row["holiday_period"]),
                    weekday = row["weekday"].ToString(),
                    order_type = "delivery",
                    method = "POST",
                });
            }              

            JObject updateHolidaySchjeduleObj = await scheduleController.AddHolidaySchedule(vendorID, holidayScheduleMetaList.ToArray());

            holiday_schedule_meta[] updateHolidaySchjeduleArr = JsonConvert.DeserializeObject<holiday_schedule_meta[]>
                (updateHolidaySchjeduleObj["holiday_schedule_meta"].ToString(),
               new JsonSerializerSettings
               {
                   NullValueHandling = NullValueHandling.Ignore,
                   DefaultValueHandling = DefaultValueHandling.Ignore
               });*/

            //임시 휴무일
            /*var vendor_id = 1001447;
            var order_type = "delivery";
            var temp_holiday_reason = "임시 휴무일";
            var start_date = "2024-05-17";
            var end_date = "2024-06-17";
            var description = "TEST";

            Models.TimeSpan opening_time = new Models.TimeSpan()
            {
                start_time = "11:00",
                end_time = "13:00"
            };

            TempScheduleMeta tempScheduleMeta = new TempScheduleMeta()
            {
                method = "POST",
                order_type = order_type,
                temp_schedule_start_date = "2024-05-17",
                temp_schedule_end_date = "2024-05-17",
                opening_time = opening_time,
            };

            JObject createTempScheduleObj = await scheduleController.UpdateTempSchedule(vendor_id, tempScheduleMeta);
            if (createTempScheduleObj == null)
            {
                Console.WriteLine("임시 영업시간 생성 실패");
                return;
            }

            List<TempScheduleMeta> createtempScheduleMetas = JsonConvert.DeserializeObject<List<TempScheduleMeta>>(createTempScheduleObj["temp_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            int result = createtempScheduleMetas
                    .Where(tempSchedule => tempSchedule.order_type == order_type
                    && tempSchedule.temp_schedule_start_date == "2024-05-17"
                    && tempSchedule.temp_schedule_end_date == "2024-05-17"
                    && tempSchedule.opening_time.start_time == opening_time.start_time
                    && tempSchedule.opening_time.end_time == opening_time.end_time).First().id;

            JObject tempScheduleObj = await scheduleController.GetTempSchedule(vendor_id);
            if (tempScheduleObj == null)
            {
                Console.WriteLine("임시 영업시간 조회 실패");
                return;
            }

            List<TempScheduleMeta> tempScheduleMetas = JsonConvert.DeserializeObject<List<TempScheduleMeta>>(tempScheduleObj["temp_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });*/

            /*int vendor_id = 1001447;
            string order_type = "delivery";
            DataTable schedule_table = new DataTable();
            schedule_table.Columns.Add("weekday", typeof(string));
            schedule_table.Columns.Add("opening_start_time", typeof(string));
            schedule_table.Columns.Add("opening_end_time", typeof(string));
            schedule_table.Columns.Add("break_time", typeof(string[]));

            schedule_table.Rows.Add("Mon", "00:00", "00:00", new string[] { });
            schedule_table.Rows.Add("Sat", "00:00", "00:00", new string[] { });
            schedule_table.Rows.Add("Sun", "00:00", "00:00", new string[] { });



            List<string> updateWeekdayList = schedule_table.AsEnumerable().Select(row => row["weekday"].ToString()).ToList();

            JObject searchWorkingScheduleObj = await scheduleController.GetWorkingSchedule(vendor_id);
            if (searchWorkingScheduleObj == null)
            {
                new Exception("영업시간 조회 실패");
                return;
            }

            RegularScheduleMeta[] currnetRegularSchjeduleArr = JsonConvert.DeserializeObject<RegularScheduleMeta[]>
                (searchWorkingScheduleObj["regular_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            List<string> currentWeekdayList = currnetRegularSchjeduleArr.AsEnumerable().Select(regularSchjedule => regularSchjedule.weekday).ToList();

            //기등록 영업시간 업데이트
            RegularScheduleMeta[] updateRegularScheduleArr = currnetRegularSchjeduleArr
                .Where(regularSchedule => updateWeekdayList.Contains(regularSchedule.weekday)).ToArray();

            foreach (RegularScheduleMeta regularScheduleMeta in updateRegularScheduleArr)
            {
                DataRow scheduleRow = schedule_table.AsEnumerable()
                    .Where(row => row["weekday"].Equals(regularScheduleMeta.weekday)).First();

                List<Models.TimeSpan> BreakTimeTimeSpanList = new List<Models.TimeSpan> { };
                foreach (string breakTimeString in (string[])scheduleRow["break_time"])
                {
                    BreakTimeTimeSpanList.Add(new Models.TimeSpan
                    {
                        start_time = breakTimeString.Split('-').First(),
                        end_time = breakTimeString.Split('-').Last()
                    });
                }
                regularScheduleMeta.order_type = order_type;
                regularScheduleMeta.opening_time = new Models.TimeSpan()
                {
                    start_time = scheduleRow["opening_start_time"].ToString(),
                    end_time = scheduleRow["opening_end_time"].ToString(),
                };
                regularScheduleMeta.break_time = BreakTimeTimeSpanList.ToArray();
                regularScheduleMeta.method = "PATCH";
            }

            //업데이트 제외 영업시간 삭제
            RegularScheduleMeta[] deleteRegularScheduleArr = currnetRegularSchjeduleArr
                .Where(regularSchedule => !updateWeekdayList.Contains(regularSchedule.weekday)).ToArray();

            foreach (RegularScheduleMeta regularScheduleMeta in deleteRegularScheduleArr)
            {
                regularScheduleMeta.method = "DELETE";
            }

            //신규 영업시간 업데이트
            var addScheduleTable = schedule_table.AsEnumerable().
                Where(scheduleRow => !currentWeekdayList.Contains(scheduleRow["weekday"].ToString()));

            List<RegularScheduleMeta> addRegularScheduleList = new List<RegularScheduleMeta>();
            foreach (var addScheduleRow in addScheduleTable)
            {
                List<Models.TimeSpan> BreakTimeTimeSpanList = new List<Models.TimeSpan> { };
                foreach (string breakTimeString in (string[])addScheduleRow["break_time"])
                {
                    BreakTimeTimeSpanList.Add(new Models.TimeSpan
                    {
                        start_time = breakTimeString.Split('-').First(),
                        end_time = breakTimeString.Split('-').Last()
                    });
                }
                addRegularScheduleList.Add(new RegularScheduleMeta
                {
                    order_type = order_type,
                    weekday = addScheduleRow["weekday"].ToString(),
                    opening_time = new Models.TimeSpan()
                    {
                        start_time = addScheduleRow["opening_start_time"].ToString(),
                        end_time = addScheduleRow["opening_end_time"].ToString(),
                    },
                    break_time = BreakTimeTimeSpanList.ToArray(),
                    method = "POST"
                });
            }

            RegularScheduleMeta[] regularScheduleMetas =
                updateRegularScheduleArr
                .Union(deleteRegularScheduleArr)
                .Union(addRegularScheduleList)
                .ToArray();

            JObject updateWorkingScheduleobj = await scheduleController.UpdateWorkingSchedule(vendor_id, regularScheduleMetas);
            if (updateWorkingScheduleobj == null)
            {
                new Exception("영업시간 업데이트 실패");
                return;
            }

            RegularScheduleMeta[] updateRegularSchjeduleArr = JsonConvert.DeserializeObject<RegularScheduleMeta[]>
                (updateWorkingScheduleobj["regular_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            DataTable UpdateResultTable = new WorkingTimeTable().ParseRegularScheduleMetaToDataTable(updateRegularSchjeduleArr);*/



            /*// 영업 중지 사유 검색
            JObject tempScheduleTypesObj = await scheduleController.GetTempScheduleTypes(temp_holiday_reason);
            if (tempScheduleTypesObj == null || tempScheduleTypesObj["results"].HasValues == false)
            {
                Console.WriteLine("영업 중지 사유 검색 실패");
                return;
            }

            JToken tempScheduleTypeToken = tempScheduleTypesObj["results"].First();

            TempScheduleType tempScheduleType = JsonConvert.DeserializeObject<TempScheduleType>(tempScheduleTypeToken.ToString(),
                    new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore,
                        DefaultValueHandling = DefaultValueHandling.Ignore
                    });

            TempHolidayScheduleMeta tempHolidayScheduleMeta = new TempHolidayScheduleMeta()
            {
                method = "POST",
                temp_schedule_type = new TempScheduleType { id = tempScheduleType.id },
                description = description == null ? "" : description,
                start_date = start_date,
                end_date = end_date,
                order_type = order_type,
            };
            JObject getTempHolidayObj = await scheduleController.GetTempHoliday(vendor_id);

            List<TempHolidayScheduleMeta> tempHolidayScheduleMetas = JsonConvert.DeserializeObject<List<TempHolidayScheduleMeta>>(getTempHolidayObj["temp_holiday_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });

            DataTable dt = TempHolidayScheduleMetasToDataTable(tempHolidayScheduleMetas);*/

            /*JObject updateTempHolidayObj = await scheduleController.UpdateTempHoliday(vendor_id, tempHolidayScheduleMeta);
            if (updateTempHolidayObj == null || updateTempHolidayObj["temp_holiday_schedule_meta"].HasValues == false)
            {
                Console.WriteLine("영업 중지 사유 등록 실패");
                return;
            }
            List<TempHolidayScheduleMeta> tempHolidayScheduleMetas = JsonConvert.DeserializeObject<List<TempHolidayScheduleMeta>>(updateTempHolidayObj["temp_holiday_schedule_meta"].ToString(),
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                });
            Console.WriteLine(tempHolidayScheduleMetas
                .Where(tempHoliday => tempHoliday.start_date == start_date
                && tempHoliday.end_date == end_date
                && tempHoliday.temp_schedule_type.id == tempScheduleType.id).First().id);*/

                Console.WriteLine(config.Token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}