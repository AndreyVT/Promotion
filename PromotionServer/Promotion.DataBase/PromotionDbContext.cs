namespace Promotion.DataBase
{
    using Microsoft.EntityFrameworkCore;
    using Promotion.Entities.Busines;
    using Promotion.Entities.Classes.DataEntities;
    using Promotion.Entities.Classes.Dictionary;
    using Promotion.Entities.Dictionary;
    
    public class PromotionDbContext: DbContext
    {
        public PromotionDbContext(DbContextOptions<PromotionDbContext> options) : base(options)
        {
            
        }

        // business entities
        public DbSet<PUser> Users { get; set; }
        public DbSet<PBonusLimit> BonusLimits { get; set; }

        // data entities
        public DbSet<PBonusTransactions> BonusTransactions { get; set; }
        public DbSet<PBonusTransactionStatusData> BonusTransactionStatusData { get; set; }

        // dictionary
        public DbSet<PBonus> Bonus { get; set; }
        public DbSet<PBonusTransactionStatus> BonusTransactionStatus { get; set; }
        public DbSet<PPeriod> Period { get; set; }
        public DbSet<PRole> Role { get; set; }
    }
}
