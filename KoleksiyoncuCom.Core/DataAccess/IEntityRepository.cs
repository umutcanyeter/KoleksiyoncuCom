using KoleksiyoncuCom.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace KoleksiyoncuCom.Core.DataAccess
{
    public interface IEntityRepository<T>
        where T : class, IEntity, new()
    {
        //An interface for data access proccess.

        //Get specific item.
        T Get(Expression<Func<T, bool>> filter = null);
        //Get list.
        List<T> GetList(Expression<Func<T, bool>> filter = null);
        //Add to database.
        void Add(T entity);
        //Update a database item.
        void Update(T entity);
        //Delete a database item.
        void Delete(T entity);
    }
}
