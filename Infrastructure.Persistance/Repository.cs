using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;
using NHibernate.Linq;

namespace Infrastructure.Persistance
{
   /// <summary>
   /// Implementation of repository using NHibernate
   /// </summary>
   /// <typeparam name="T"></typeparam>
   public class Repository<T> : IRepository<T>
   {
      private readonly ISession m_session;

      public Repository(ISession a_session)
      {
         m_session = a_session;
      }

      private ISession CurrentSession
      {
         get { return m_session; }
      }

      #region IRepository<T> Members

      public void MakePersistent(params T[] a_entities)
      {
         foreach (T entity in a_entities)
         {
            CurrentSession.Save(entity);
         }
      }

      public T GetById(long a_id)
      {
         return CurrentSession.Get<T>(a_id);
      }

      public IQueryable<T> Retrieve(Expression<Func<T, bool>> a_criteria)
      {
         return CurrentSession
            .Query<T>().Where(a_criteria);
      }

      public IQueryable<T> RetrieveAll()
      {
         return CurrentSession.Query<T>();
      }

      #endregion
   }
}