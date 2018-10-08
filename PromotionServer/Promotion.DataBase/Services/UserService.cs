namespace Promotion.DataBase.Services
{
    using Promotion.DataBase.Interfaces;
    using Promotion.Entities.Busines;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserService : BaseService, IUserService<PUser, int>
    {
        public UserService(PromotionDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(PUser user)
        {
            this._dbContext.Add(user);
        }

        public new IEnumerable<PUser> Get()
        {
            return this._dbContext.Users.ToList();
        }

        public new PUser GetById(int id)
        {
            return this._dbContext.Users.FirstOrDefault(c => c.Id == id);
        }

        public new PUser Update(int entity)
        {
            throw new NotImplementedException();
        }
    }
}
