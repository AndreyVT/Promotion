namespace Promotion.Common
{
    public class IdentityServerUserInfo
    { // // "id":"admin","userName":"admin","normalizedUserName":"ADMIN","email":null,"normalizedEmail":null,"emailConfirmed":false

        public string id { get; set; }

        public string userName { get; set; }

        public string normalizedUserName { get; set; }

        public string email { get; set; }

        public bool emailConfirmed { get; set; }
    }
}
