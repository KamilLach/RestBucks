using System.Linq;
using Application;
using Application.Dto;
using Domain;
using Infrastructure.Exceptions;
using NUnit.Framework;
using SharpTestsEx;
using Test.Domain.Mocks;

namespace Test.Domain
{
   public class GivenAReadyOrder
   {
      private OrderDto m_dto;
      private Order m_order;

      [SetUp]
      public void SetUp()
      {
         var mapper = new DtoMapper(new LinkProvider());
         m_order = new Order();
         m_order.Pay("123", "jose");
         m_order.Finish();
         m_dto = mapper.Map<Order, OrderDto>(m_order);
      }

      [Test]
      public void ThenNextStepsIncludeGet()
      {
         m_dto.Links
            .Satisfy(
               a_links =>
               a_links.Any(a_l => a_l.Uri == "http://restbuckson.net/order/0/receipt" && a_l.Relation.EndsWith("docs/receipt-coffee.htm")));
      }

      [Test]
      public void CancelShouldThrow()
      {
         m_order.Executing(a_o => a_o.Cancel("error"))
            .Throws<InvalidOrderOperationException>()
            .And
            .Exception.Message.Should().Be.EqualTo("The order can not be canceled because it is ready.");
      }

      [Test]
      public void PayShouldThrow()
      {
         m_order.Executing(a_o => a_o.Pay("a", "b"))
            .Throws<InvalidOrderOperationException>()
            .And
            .Exception.Message.Should().Be.EqualTo("The order can not be paid because it is ready.");
      }
   }
}