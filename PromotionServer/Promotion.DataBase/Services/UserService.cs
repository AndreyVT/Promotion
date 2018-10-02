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

        public IEnumerable<PUser> Get()
        {
            return this._dbContext.Users.ToList();
        }

        public PUser GetById(int id)
        {
            return this._dbContext.Users.FirstOrDefault(c => c.Id == id);
        }

        public void Remove(int id)
        {
            var user = this.GetById(id);
            if (user != null)
            {
                this._dbContext.Users.Remove(user);
            }
        }

        public PUser Update(int entity)
        {
            throw new NotImplementedException();
        }
    }
}
