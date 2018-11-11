namespace Promotion.DataBase.Services
{
    using Promotion.DataBase.Base;
    using Promotion.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class UserRepository : BaseRepository, IBaseRepository<PUser, int>
    {
        public UserRepository(PromotionDbContext dbContext) : base(dbContext)
        {
        }

        public PUser Add(PUser user)
        {
            return (PUser)base.Add(user);
        }

        public new IEnumerable<PUser> Get()
        {
            return _dbContext.Set<PUser>();
        }

        public new PUser GetById(int id)
        {
            return this._dbContext.Set<PUser>().FirstOrDefault(c => c.Id == id);
        }

        public PUser Update(PUser entity)
        {
            var result = base.Update(entity);

            return (PUser)base.GetById(result.Id);
        }
    }
}
