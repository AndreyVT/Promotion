namespace Promotion.DataBase.Base
{
    using System.Collections.Generic;
     
    public interface IBaseRepository<TEntity, TEntityId>
    {
        TEntity Add(TEntity entity);
        TEntity Update(TEntity entity);
        IEnumerable<TEntity> Get();
        TEntity GetById(TEntityId id);
        void Remove(TEntityId id);
    }
}
