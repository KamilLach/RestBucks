using System.Linq;
using Application;
using Application.Dto;
using Domain;
using Infrastructure.Exceptions;
using NUnit.Framework;
using SharpTestsEx;

namespace Test.Domain
{
   [TestFixture]
   public class GivenAPayedOrder
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         var mapper = new DtoMapper();
         m_order = new Order();
         m_order.Pay("123", "jose");
         m_dto = mapper.Map<Order, OrderDto>(m_order);
      }

      #endregion

      private Order m_order;
      private OrderDto m_dto;

      [Test]
      public void CancelShouldThrow()
      {
         m_order.Executing(o => o.Cancel("error"))
            .Throws<InvalidOrderOperationException>()
            .And
            .Exception.Message.Should().Be.EqualTo("The order can not be canceled because it is paid.");
      }

      [Test]
      public void PayShouldThrow()
      {
         m_order.Executing(o => o.Pay("123", "jes"))
            .Throws<InvalidOrderOperationException>()
            .And
            .Exception.Message.Should().Be.EqualTo("The order can not be paid because it is paid.");
      }

      [Test]
      public void ThenNextStepsIncludeGet()
      {
         m_dto.Links.Satisfy(a_links => a_links.Any(a_l => a_l.Uri == "http://restbuckson.net/order/0" && a_l.Relation.EndsWith("docs/order-get.htm")));
      }
   }
}