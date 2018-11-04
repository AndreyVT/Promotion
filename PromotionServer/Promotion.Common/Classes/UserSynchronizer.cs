namespace Promotion.Common.Classes
{
    using Microsoft.Extensions.Configuration;
    using Promotion.Common.Interfaces;
    using Promotion.DataBase;

    public class UserSynchronizer: IUserSynchronizer
    {
        private readonly PromotionDbContext _promotionDbContext;
        private readonly IConfiguration _configuration;

        public UserSynchronizer(PromotionDbContext promotionDbContext, IConfiguration configuration)
        {
            _promotionDbContext = promotionDbContext;
            _configuration = configuration;
        }

        public void SyncUsers()
        {
           
        }
    }
}
