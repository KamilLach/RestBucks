using System;
using System.Web;
using NHibernate;

namespace Infrastructure.Persistance
{
   public class TransactionTracker : IDisposable
   {
      #region Fields

      private bool m_disposed;

      #endregion

      #region Properties

      public ITransaction CurrentTransaction { get; set; }

      #endregion

      #region Implementation of IDisposable

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      /// <filterpriority>2</filterpriority>
      public void Dispose()
      {
         if (m_disposed)
         {
            return;
         }

         m_disposed = true;

         if (CurrentTransaction == null)
         {
            return;
         }

         if (HttpContext.Current.Error != null)
         {
            CurrentTransaction.Rollback();
         }
         else
         {
            CurrentTransaction.Commit();
         }
      }

      #endregion
   }
}