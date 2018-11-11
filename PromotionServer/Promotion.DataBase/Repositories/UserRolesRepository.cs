namespace Promotion.DataBase.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Promotion.DataBase.Base;
    using Promotion.DataBase.Services;
    using Promotion.Domain.Entities;

    public class UserRolesRepository : BaseRepository, IBaseRepository<PUserRole, int>
    {
        public UserRolesRepository(PromotionDbContext dbContext) : base(dbContext)
        {
        }

        public PUserRole Add(PUserRole userRole)
        {
            return (PUserRole)base.Add(userRole);
        }

        public PUserRole Update(PUserRole entity)
        {
            return (PUserRole)base.Update(entity);
        }

        public new IEnumerable<PUserRole> Get()
        {
            return _dbContext.Set<PUserRole>().Include(c => c.User).Include(c => c.Role);
        }

        public new PUserRole GetById(int id)
        {
            return this.Get().FirstOrDefault(c => c.Id == id);
        }
    }
}
