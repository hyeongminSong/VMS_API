using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;
using ConsoleApp1.Models;

namespace ConsoleApp1
{
    internal class TokenController
    {
        private readonly HttpClient _httpClient;
        private readonly UserData user;
        public Config config;

        public TokenController(Config config, UserData user)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(config.URL)
            };
            this.user = user;
            this.config = config;
            _httpClient.DefaultRequestHeaders.Add("accept", "application/json");
        }

        public async Task<Config> GetAccessToken()
        {
            var tokenEndpoint = "/auth/login/token/"; // Token 발급 엔드포인트
            var jsonContent = JsonContent.Create(user);

            using (var response = await _httpClient.PostAsync(tokenEndpoint, jsonContent))
            {
                if (response.IsSuccessStatusCode)
                {
                    var tokenResponse = await response.Content.ReadAsStringAsync();
                    // 여기에서 적절한 방식으로 토큰 추출 및 반환
                    dynamic jObj = JObject.Parse(tokenResponse);
                    tokenResponse = Convert.ToString(jObj.access_token);
                    config.Token = tokenResponse;
                    return config;
                }

                throw new Exception("Failed to obtain access token.");
            }
        }
    }
}
