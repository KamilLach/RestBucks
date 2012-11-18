using System.Linq;
using Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Modules;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace Test.Data
{
    [TestFixture, Ignore]
    public class DataInitializer
    {
        private ISessionFactory m_sessionFactory;

        [TestFixtureSetUp]
        public void SetUp()
        {
            m_sessionFactory = Fluently.Configure()
             .Database(MsSqlConfiguration.MsSql2008.ConnectionString(a_c => a_c.FromConnectionStringWithKey("Restbucks")))
             .Mappings(a_m => a_m.FluentMappings.AddFromAssemblyOf<NHibernateModule>())
             .ExposeConfiguration(a_c => new SchemaExport(a_c).Execute(true, true, false))
             .BuildSessionFactory();
        }

        [Test, Ignore]
        public void InitializeData()
        {
            using (var session = m_sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {

                var milk = new Customization
                               {
                                   Name = "Milk",
                                   PossibleValues = { "skim", "semi", "whole" }
                               };

                var size = new Customization
                               {
                                   Name = "Size",
                                   PossibleValues = { "small", "medium", "large" }
                               };

                var shots = new Customization
                                {
                                    Name = "Shots",
                                    PossibleValues = { "single", "double", "triple" }
                                };

                var whippedCream = new Customization
                                       {
                                           Name = "Whipped Cream",
                                           PossibleValues = { "yes", "no" }
                                       };

                var kindOfCookie = new Customization
                                       {
                                           Name = "Kind",
                                           PossibleValues = { "chocolate chip", "ginger" }
                                       };


                var customizationRepository = new Repository<Customization>(session);
                customizationRepository.MakePersistent(milk, size, shots, whippedCream, kindOfCookie);
                var productRepository = new Repository<Product>(session);

                var coffees = new[] { "Latte", "Capuccino", "Espresso", "Tea" }
                    .Select(coffeName => new Product { Name = coffeName, Price = (decimal)coffeName.First() / 10, Customizations = { milk, size, shots } })
                    .ToArray();

                productRepository.MakePersistent(coffees);

                productRepository.MakePersistent(new Product { Name = "Hot Chocolate", Price = 10.5m, Customizations = { milk, size, whippedCream } });
                productRepository.MakePersistent(new Product { Name = "Cookie", Price = 1, Customizations = { kindOfCookie } });
                tx.Commit();
            }
        }
    }
}