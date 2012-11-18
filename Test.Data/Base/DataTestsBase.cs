using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Persistance.Modules;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Test.Data.Base
{
   public class DataTestsBase
   {
      protected ISessionFactory m_sessionFactory;
      private Configuration m_configuration;

      [TestFixtureSetUp]
      public void FixtureSetUp()
      {
         m_sessionFactory = Fluently.Configure()
            //.Database(MsSqlConfiguration.MsSql2008.ConnectionString(a_c => a_c.FromConnectionStringWithKey("Restbucks_Test"))
            .Database(SQLiteConfiguration.Standard.UsingFile("restbucks_test.db"))
            .Mappings(a_m => a_m.FluentMappings.AddFromAssemblyOf<NHibernateModule>())
            .ExposeConfiguration(a_c =>
                                    {
                                       m_configuration = a_c;
                                       new SchemaExport(a_c).Execute(true, true, false);
                                    })
            .BuildSessionFactory();

         m_sessionFactory = m_configuration.BuildSessionFactory();
      }

      [TestFixtureTearDown]
      public void TearDown()
      {
         new SchemaExport(m_configuration).Execute(true, true, true);
      }
   }
}