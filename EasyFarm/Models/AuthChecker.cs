using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace EasyFarm.Models
{
    public class AuthChecker
    {
         
        private static readonly AuthChecker instance = new AuthChecker();

        public static AuthChecker Instance
        {
            get
            {
                return instance;
            }
        }


        public string chechAuth(string token)
        {
            TokenInfo tokenInfo = GetTokenInfoAsync(token).Result;
            if (tokenInfo == null || !tokenInfo.azp.Equals(Config.client_id) || string.IsNullOrWhiteSpace(tokenInfo.sub))
            {
                return string.Empty;
            }        
            return tokenInfo.sub;
        }

        static async Task<TokenInfo> GetTokenInfoAsync(string token)
        {
            TokenInfo tokenInfo = null;
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://www.googleapis.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

            HttpResponseMessage response =  client.GetAsync("/oauth2/v3/tokeninfo?access_token="+ token).Result;
            if (response.IsSuccessStatusCode)
            {
                tokenInfo = await  response.Content.ReadAsAsync<TokenInfo>();
            }
            return tokenInfo;
        }
    }


    public class TokenInfo
    {
        public string azp { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string scope { get; set; }
        public string exp { get; set; } 
        public string expires_in { get; set; }
        public string access_type { get; set; }
               
    }
}