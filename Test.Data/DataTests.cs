using System.Linq;
using Domain;
using Infrastructure.Persistance;
using NUnit.Framework;
using SharpTestsEx;
using Test.Data.Base;

namespace Test.Data
{
    [TestFixture]
    public class DataTests : DataTestsBase
    {
        [Test]
        public void CanStoreACustomization()
        {
           long customizationId;
            using (var session = m_sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                var repository = new Repository<Customization>(session);
                var customization = new Customization { Name = "Milk", PossibleValues = { "skim", "semi", "whole" } };
                repository.MakePersistent(customization);
                customizationId = customization.Id;
                tx.Commit();
            }
            using (var session = m_sessionFactory.OpenSession())
            {
                var repository = new Repository<Customization>(session);
                Customization readed = repository.GetById(customizationId);
                readed.Satisfy(c => c.Name == "Milk" && c.PossibleValues.SequenceEqual(new[] { "skim", "semi", "whole" }));
            }
        }

        [Test]
        public void CanStoreAProduct()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<CoffeeShopContext>());
            long id;
            using (var session = m_sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var repository = new Repository<Product>(session);
                var product = new Product
                {
                    Name = "Coffee 1",
                    Price = 10.4m
                };

                repository.MakePersistent(product);
                id = product.Id;
                tx.Commit();
            }

            using (var session = m_sessionFactory.OpenSession())
            using (session.BeginTransaction())
            {
                var repository = new Repository<Product>(session);
                Product product = repository.GetById(id);

                product.Satisfy(p => p.Name == "Coffee 1" && p.Price == 10.4m);
            }
        }

        [Test]
        public void CanStoreOneCustomizationInTwoProducts()
        {
            long customizationId;
            using (var session = m_sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customizationRepository = new Repository<Customization>(session);
                var customization = new Customization { Name = "Milk", PossibleValues = { "skim", "semi", "whole" } };
                customizationRepository.MakePersistent(customization);
                customizationId = customization.Id;

                var productRepository = new Repository<Product>(session);
                productRepository.MakePersistent(new Product
                {
                    Name = "Coffee 3",
                    Price = 10.4m,
                    Customizations = { customization }
                });
                productRepository.MakePersistent(new Product { Name = "Coffee 4", Price = 5.4m, Customizations = { customization } });

                tx.Commit();
            }

            using (var session = m_sessionFactory.OpenSession())
            {
                new Repository<Product>(session)
                    .Retrieve(p => p.Customizations.Any(c => c.Id == customizationId))
                    .Count().Should().Be.EqualTo(2);
            }
        }

        [Test]
        public void CanStoreTwoProducts()
        {
            using (var session = m_sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var repository = new Repository<Product>(session);

                repository.MakePersistent(new Product { Name = "Coffee 3", Price = 10.4m });
                repository.MakePersistent(new Product { Name = "Coffee 4", Price = 5.4m });

                tx.Commit();
            }
        }
    }
}