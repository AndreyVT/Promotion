namespace Promotion.DataBase.Base
{
    using System.Collections.Generic;
     
    public interface IBaseService<TEntity, TEntityId>
    {
        void Add(TEntity entity);
        TEntity Update(TEntityId entity);
        IEnumerable<TEntity> Get();
        TEntity GetById(TEntityId id);
        void Remove(TEntityId id);
    }
}
