using System;
using System.Linq;
using System.Linq.Expressions;

namespace Infrastructure
{
   /// <summary>
   /// Base generic repository
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public interface IRepository<T>
   {
      void MakePersistent(params T[] a_entities);
      T GetById(long a_id);
      IQueryable<T> Retrieve(Expression<Func<T, bool>> a_criteria);
      IQueryable<T> RetrieveAll();
   }
}