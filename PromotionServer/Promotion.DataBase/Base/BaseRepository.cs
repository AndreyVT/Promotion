namespace Promotion.DataBase.Services
{
    using Promotion.DataBase.Base;
    using Promotion.Domain.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class BaseRepository : IBaseRepository<IBaseEntity, int>
    {
        internal PromotionDbContext _dbContext;

        public BaseRepository(PromotionDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBaseEntity Add(IBaseEntity entity)
        {
            var result = _dbContext.Add<IBaseEntity>(entity);
            _dbContext.SaveChanges();

            return result.Entity;
        }

        public IEnumerable<IBaseEntity> Get()
        {
            return _dbContext.Set<IBaseEntity>();
        }

        public IBaseEntity GetById(int id)
        {
            IBaseEntity entity = _dbContext.Set<IBaseEntity>().FirstOrDefault(c => c.Id == id);

            return entity;
        }

        public virtual void Remove(int id)
        {
            IBaseEntity entity = _dbContext.Set<IBaseEntity>().FirstOrDefault(c => c.Id == id);

            if (entity != null)
            {
                _dbContext.Set<IBaseEntity>().Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public IBaseEntity Update(IBaseEntity entity)
        {
            IBaseEntity result = _dbContext.Set<IBaseEntity>().Update(entity).Entity;
            _dbContext.SaveChanges();

            return result;
        }
    }
}
