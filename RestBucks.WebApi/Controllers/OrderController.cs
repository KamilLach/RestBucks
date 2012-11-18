

using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application.Dto;
using Domain;
using Infrastructure;
using Infrastructure.HyperMedia.Linker.Interfaces;
using RestBucks.WebApi.Models;

namespace RestBucks.WebApi.Controllers
{
   public class OrderController : ApiController
   {
      #region Fields

      private readonly IRepository<Order> m_orderRepository;
      private readonly IDtoMapper m_dtoMapper;
      private readonly IResourceLinker m_resourceLinker;

      #endregion

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller"/> class.
      /// </summary>
      public OrderController(IRepository<Order> a_orderRepository, IDtoMapper a_dtoMapper,
         IResourceLinker a_resourceLinker)
      {
         if (a_orderRepository == null)
         {
            throw new ArgumentNullException("a_orderRepository");
         }

         if (a_dtoMapper == null)
         {
            throw new ArgumentNullException("a_dtoMapper");
         }

         m_orderRepository = a_orderRepository;
         m_dtoMapper = a_dtoMapper;
         m_resourceLinker = a_resourceLinker;
      }

      public HttpResponseMessage Get(int a_orderId)
      {
         Order order = m_orderRepository.GetById(a_orderId);
         if (order == null)
         {
            return new HttpResponseMessage(HttpStatusCode.NotFound);
         }

         return Request.CreateResponse(HttpStatusCode.Accepted, m_dtoMapper.Map<Order, OrderDto>(order));
      }

      //public HttpResponseMessage GetReceipt(int a_orderId)
      //{
      //   Order order = m_orderRepository.GetById(a_orderId);
      //   if (order == null)
      //   {
      //      return new HttpResponseMessage(HttpStatusCode.NotFound);
      //   }

      //   return Request.CreateResponse(HttpStatusCode.Accepted, m_dtoMapper.Map<Order, OrderDto>(order));
      //}

      public HttpResponseMessage Post(OrderLocationDto a_orderDtoModel)
      {
         Order order = m_orderRepository.GetById(a_orderDtoModel.OrderId);
         if (order == null)
         {
            return Request.CreateResponse(HttpStatusCode.NotFound);
         }

         order.Location = a_orderDtoModel.Location;
         return Request.CreateResponse(HttpStatusCode.OK);
      }

      public HttpResponseMessage PostPay(int a_orderId, PaymentDto a_paymentDto)
      {
         return Request.CreateResponse(HttpStatusCode.NotFound);
      }

      public HttpResponseMessage Delete(int a_orderId)
      {
         var order = m_orderRepository.GetById(a_orderId);
         if (order == null)
         {
            return Request.CreateResponse(HttpStatusCode.NotFound);
         }

         order.Cancel("canceled from the rest interface");
         return Request.CreateResponse(HttpStatusCode.OK);
      }
   }
}
