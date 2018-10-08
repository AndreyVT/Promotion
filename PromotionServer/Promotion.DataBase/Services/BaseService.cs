namespace Promotion.DataBase.Services
{
    using Promotion.DataBase.Base;
    using Promotion.Entities.Interfaces.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseService: IBaseService<IBaseEntity, int>
    {
        internal PromotionDbContext _dbContext;

        public BaseService(PromotionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(IBaseEntity entity)
        {
            
        }

        public IEnumerable<IBaseEntity> Get()
        {
            throw new System.NotImplementedException();
        }

        public IBaseEntity GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public virtual void Remove(int id)
        {
            _dbContext.Set<IBaseEntity>().FirstOrDefault(c => c.Id == id);
            //_dbContext.Remove
        }

        public IBaseEntity Update(IBaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public IBaseEntity Update(int entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
