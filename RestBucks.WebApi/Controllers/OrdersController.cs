using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using Application.Dto;
using Domain;
using Domain.BaseClass;
using Infrastructure;
using Infrastructure.Persistance.Extensions;
using RestBucks.WebApi.Models;

namespace RestBucks.WebApi.Controllers
{
   public class OrdersController : ApiController
   {
      #region Fields

      private readonly IDtoMapper m_dtoMapper;
      private readonly IRepository<Order> m_orderRepository;
      private readonly IRepository<Product> m_productRepository;

      #endregion

      #region Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Web.Mvc.Controller"/> class.
      /// </summary>
      public OrdersController(IRepository<Order> a_orderRepository,
         IRepository<Product> a_productRepository, IDtoMapper a_dtoMapper)
      {
         if (a_orderRepository == null)
         {
            throw new ArgumentNullException("a_orderRepository");
         }

         if (a_productRepository == null)
         {
            throw new ArgumentNullException("a_productRepository");
         }

         if (a_dtoMapper == null)
         {
            throw new ArgumentNullException("a_dtoMapper");
         }

         m_orderRepository = a_orderRepository;
         m_productRepository = a_productRepository;
         m_dtoMapper = a_dtoMapper;
      }

      #endregion

      /// <summary>
      /// Creates an order
      /// </summary>
      /// <param name="a_orderModel">Order dto model</param>
      /// <remarks>
      /// Json Invoke: {Location: "inShop", Items: {Name: "latte", Quantity: 5}}
      /// Xml invoke: 
      /// </remarks>
      /// <returns>Response</returns>
      public HttpResponseMessage Post(OrderDto a_orderModel)
      {
         var order = new Order
         {
            Date = DateTime.Today,
            Location = a_orderModel.Location
         };

         foreach (var requestedItem in a_orderModel.Items)
         {
            var product = m_productRepository.GetByName(requestedItem.Name);
            if (product == null)
            {
               return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format("We don't offer {0}", requestedItem.Name));
            }

            var orderItem = new OrderItem(product,
                                        requestedItem.Quantity,
                                        product.Price,
                                        requestedItem.Preferences.ToDictionary(a_x => a_x.Key, a_y => a_y.Value));
            order.AddItem(orderItem);
         }

         if (!order.IsValid())
         {
            var content = string.Join("\n", order.GetErrorMessages());
            return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format("Invalid entities values {0}", content));
         }

         m_orderRepository.MakePersistent(order);
         //var uri = resourceLinker.GetUri<OrderResourceHandler>(orderResource => orderResource.Get(0, null), new { orderId = order.Id });
         return Request.CreateResponse(HttpStatusCode.OK);
      }
   }
}