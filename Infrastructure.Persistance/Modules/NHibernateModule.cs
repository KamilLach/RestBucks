using System.IO;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Infrastructure.Persistance.Modules
{
   public class NHibernateModule : Module
   {
      #region Private methods

      /// <summary>
      /// Configure session Factory
      /// </summary>
      /// <returns>Session</returns>
      private ISessionFactory CreateSessionFactory(IComponentContext a_componentContext)
      {
         return Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2008.ConnectionString(a_c => a_c.FromConnectionStringWithKey("Restbucks")))
            .Mappings(a_m => a_m.FluentMappings.AddFromAssemblyOf<NHibernateModule>())
            .BuildSessionFactory();
      }

      #endregion

      #region Overrides of Module

      /// <summary>
      /// Override to add registrations to the container.
      /// </summary>
      /// <remarks>
      /// Note that the ContainerBuilder parameter is unique to this module.
      /// </remarks>
      /// <param name="a_builder">The builder through which components can be
      ///             registered.</param>
      protected override void Load(ContainerBuilder a_builder)
      {
         a_builder.RegisterGeneric(typeof(Repository<>))
               .As(typeof(IRepository<>))
               .InstancePerApiRequest();

         a_builder.Register(a_c => new TransactionTracker())
             .InstancePerApiRequest();

         a_builder.Register(a_c => a_c.Resolve<ISessionFactory>().OpenSession())
             .InstancePerApiRequest()
             .OnActivated(a_e => a_e.Context.Resolve<TransactionTracker>().CurrentTransaction = a_e.Instance.BeginTransaction());

         a_builder.Register(CreateSessionFactory).SingleInstance();

         base.Load(a_builder);
      }

      #endregion
   }
}