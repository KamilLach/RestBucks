using System.Linq;
using Application;
using Application.Dto;
using Domain;
using NUnit.Framework;
using SharpTestsEx;
using Test.Domain.Mocks;

namespace Test.Domain
{
   [TestFixture]
   public class GivenAnUnpaidOrder
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         var mapper = new DtoMapper(new LinkProvider());
         m_order = new Order();
         m_dto = mapper.Map<Order, OrderDto>(m_order);
      }

      #endregion

      private Order m_order;
      private OrderDto m_dto;

      [Test]
      public void CancelShouldWork()
      {
         m_order.Cancel("error");
         m_order.Status.Should().Be.EqualTo(OrderStatus.Canceled);
      }

      [Test]
      public void NextStepShouldNotIncludeReceipt()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               !a_links.Any(
                  a_l => a_l.Uri == "http://restbuckson.net/order/ready/0" && a_l.Relation.EndsWith("docs/receipt-coffee.htm")));
      }

      [Test]
      public void PayShouldWork()
      {
         m_order.Pay("Jose", "123123123");
         m_order.Status.Should().Be.EqualTo(OrderStatus.Paid);
      }

      [Test]
      public void TheNextStepsIncludePay()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               a_links.Any(
                  a_l =>
                  a_l.Uri == "http://restbuckson.net/order/0/payment" && a_l.Relation.EndsWith("docs/order-pay.htm")));
      }

      [Test]
      public void TheNextStepsIncludeUpdate()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               a_links.Any(
                  a_l => a_l.Uri == "http://restbuckson.net/order/0" && a_l.Relation.EndsWith("docs/order-update.htm")));
      }

      [Test]
      public void ThenNextStepsIncludeCancel()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               a_links.Any(
                  l => l.Uri == "http://restbuckson.net/order/0" && l.Relation.EndsWith("docs/order-cancel.htm")));
      }

      [Test]
      public void ThenNextStepsIncludeGet()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               a_links.Any(l => l.Uri == "http://restbuckson.net/order/0" && l.Relation.EndsWith("docs/order-get.htm")));
      }
   }
}