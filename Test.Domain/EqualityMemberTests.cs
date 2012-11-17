using Domain;
using NUnit.Framework;
using SharpTestsEx;

namespace Test.Domain
{
   [TestFixture]
   public class EqualityMemberTests
   {
      [Test]
      public void WhenComparingToNullShouldBeFalse()
      {
         new Product().Equals(null).Should().Be(false);
      }

      [Test]
      public void WhenComparingToOtherTransientShouldBeFalse()
      {
         new Product().Equals(new Product())
            .Should().Be(false);
      }

      [Test]
      public void WhenSameTransientShouldBeTrue()
      {
         var product = new Product();
         product.Equals(product).Should().Be(true);
      }

      [Test]
      public void WhenComparingPersistedToOtherTransientShouldBeFalse()
      {
         new Product {Id = 123}.Equals(new Product())
            .Should().Be(false);
      }

      [Test]
      public void WhenComparingTwoPersistedWithSameIdThenShouldBeTrue()
      {
         new Product {Id = 123}.Equals(new Product {Id = 123}).Should().Be(true);
      }

      [Test]
      public void WhenComparingSameInstanceOfPersistedEntityThenShouldBeTrue()
      {
         var product = new Product { Id = 123 };
         product.Equals(product)
            .Should().Be(true);
      }
   }
}