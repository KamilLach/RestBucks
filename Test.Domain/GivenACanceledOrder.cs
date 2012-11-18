using Application;
using Application.Dto;
using Domain;
using Infrastructure.Exceptions;
using NUnit.Framework;
using SharpTestsEx;
using Test.Domain.Mocks;

namespace Test.Domain
{
   [TestFixture]
   public class GivenACanceledOrder
   {
      private Order m_order;
      private OrderDto m_dto;
      [SetUp]
      public void SetUp()
      {
         DtoMapper mapper = new DtoMapper(new LinkProvider());
         m_order = new Order();
         m_order.Cancel("You are too slow.");
         m_dto = mapper.Map<Order, OrderDto>(m_order);
      }

      [Test]
      public void NextStepsShouldBeEmpty()
      {
         m_dto.Links.Should().Be.Empty();
      }

      [Test]
      public void CancelShouldThrow()
      {
         m_order.Executing(a_o => a_o.Cancel("error"))
             .Throws<InvalidOrderOperationException>()
             .And
             .Exception.Message.Should().Be.EqualTo("The order can not be canceled because it is canceled.");
      }
      [Test]
      public void AddItemShouldThrow()
      {
         m_order.Executing(a_o => a_o.AddItem(new OrderItem()))
             .Throws<InvalidOrderOperationException>()
             .And
             .Exception.Message.Should().Be.EqualTo("Can't add another item to the order because it is canceled.");
      }
      [Test]
      public void PayShouldThrow()
      {
         m_order.Executing(a_o => a_o.Pay("a", "b"))
             .Throws<InvalidOrderOperationException>()
             .And
             .Exception.Message.Should().Be.EqualTo("The order can not be paid because it is canceled.");
      }
   }
}