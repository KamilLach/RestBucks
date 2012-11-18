using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Application.Dto;
using Domain;
using Infrastructure;

namespace RestBucks.WebApi.Controllers
{
   public class OrdersController : ApiController
   {
      #region Fields

      private readonly IDtoMapper m_dtoMapper;
      private readonly IRepository<Order> m_orderRepository;

      #endregion

      #region Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller"/> class.
      /// </summary>
      public OrdersController(IRepository<Order> a_orderRepository, IDtoMapper a_dtoMapper)
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
      }

      #endregion

      /// <summary>
      /// Creates an order
      /// </summary>
      /// <param name="a_orderId"></param>
      /// <param name="a_orderDto"></param>
      /// <returns>Response</returns>
      public HttpResponseMessage Post(int a_orderId, OrderDto a_orderDto)
      {
         return Request.CreateResponse(HttpStatusCode.NotFound);
      }
   }
}