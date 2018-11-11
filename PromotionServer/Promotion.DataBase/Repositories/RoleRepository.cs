namespace Promotion.DataBase.Repositories
{
    using System.Collections.Generic;
    using Promotion.DataBase.Base;
    using Promotion.DataBase.Services;
    using Promotion.Domain.Entities;

    public class RoleRepository : BaseRepository, IBaseRepository<PRole, int>
    {
        public RoleRepository(PromotionDbContext dbContext) : base(dbContext)
        {
        }

        public PRole Add(PRole role)
        {
            return (PRole)base.Add(role);
        }

        public PRole Update(PRole role)
        {
            return (PRole)base.Update(role);
        }

        public new IEnumerable<PRole> Get()
        {
            return _dbContext.Set<PRole>();
        }

        public new PRole GetById(int id)
        {
            return (PRole)base.GetById(id);
        }
    }
}
