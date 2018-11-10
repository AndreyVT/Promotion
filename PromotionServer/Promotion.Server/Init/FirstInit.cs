using Promotion.Common.Dictionaries;
using Promotion.DataBase;

namespace Promotion.Server.Init
{
    public class FirstInit
    {
        private PromotionDbContext dbContext;

        public FirstInit(PromotionDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Init()
        {
            AddRoles();
        }

        private void AddRoles()
        {
            this.dbContext.Role.Add(Roles.Admin);
            this.dbContext.Role.Add(Roles.Manager);
            this.dbContext.Role.Add(Roles.User);
            this.dbContext.SaveChanges();
        }
    }
}
