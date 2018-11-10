namespace Promotion.Common.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using IdentityModel.Client;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using Promotion.Common.DomainEntities;
    using Promotion.Common.Interfaces;
    using Promotion.DataBase;
    

    public class UserSynchronizer: IUserSynchronizer
    {
        private readonly PromotionDbContext _promotionDbContext;
        private readonly IConfiguration _configuration;

        private readonly string identityServerUrl;
        private readonly string identityServiceClientId;
        private readonly string identityServiceClientSecret;
        private readonly string identityServiceGrantType;
        
        private readonly string login;
        private readonly string password;

        public UserSynchronizer(PromotionDbContext promotionDbContext, IConfiguration configuration)
        {
            _promotionDbContext = promotionDbContext;
            _configuration = configuration;

            var identityServerSection = _configuration.GetSection("IdentityServer");
            identityServerUrl = identityServerSection.GetValue<string>("Host");
            identityServiceClientId = identityServerSection.GetValue<string>("IdentityServiceClientId");
            identityServiceClientSecret = identityServerSection.GetValue<string>("IdentityServiceClientSecret");
            identityServiceGrantType = identityServerSection.GetValue<string>("IdentityServiceGrantType");

            var defaultAdminUserSection = _configuration.GetSection("DefaultAdminUser");
            login = defaultAdminUserSection.GetValue<string>("Login");
            password = defaultAdminUserSection.GetValue<string>("Password");
        }

        public async Task<IdentityServerUserInfo> GetUserInfo(string userLogin)
        {
            // string body = $"client_id={identityServiceClientId}&client_secret={identityServiceClientSecret}&grant_type={identityServiceGrantType}&username={login}&password={password}";
            
            // 'Content-Type': 'application/x-www-form-urlencoded'
            
            /* try
            {
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
            }
            catch(Exception ex)
            {  // Misused header name. Make sure request headers are used with HttpRequestMessage, response headers with HttpResponseMessage, and content headers with HttpContent objects.
                string message = ex.Message;
            }

            List<KeyValuePair<string, string>> nameValueCollection = new List<KeyValuePair<string, string>>();
            nameValueCollection.Add(new KeyValuePair<string, string>("client_id", identityServiceClientId));
            nameValueCollection.Add(new KeyValuePair<string, string>("client_secret", identityServiceClientSecret));
            nameValueCollection.Add(new KeyValuePair<string, string>("grant_type", identityServiceGrantType));
            nameValueCollection.Add(new KeyValuePair<string, string>("username", login));
            nameValueCollection.Add(new KeyValuePair<string, string>("password", password));

            StringContent stringContent = new StringContent(body);
            FormUrlEncodedContent formUrlEncodedContent = new FormUrlEncodedContent(nameValueCollection);

            stringContent.Headers.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");*/

            // var result = httpClient.PostAsync($"{identityServerUrl}/connect/token", formUrlEncodedContent).Result;
            var disco = await DiscoveryClient.GetAsync(identityServerUrl);
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return null;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, identityServiceClientId, identityServiceClientSecret);
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(login, password, "api1");
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return null;
            }

            HttpClient httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            
            var res = httpClient.GetAsync($"{identityServerUrl}/api/user/getByLogin/{userLogin}").Result;
            if (res.IsSuccessStatusCode)
            {
                string userInfo = res.Content.ReadAsStringAsync().Result;
                IdentityServerUserInfo rr = JsonConvert.DeserializeObject<IdentityServerUserInfo>(userInfo);
                return rr;
            }

            return null;
        }

        public void SyncUsers()
        {
           
        }
    }
}
