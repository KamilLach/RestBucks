using System;
using Application.Dto;
using AutoMapper;
using Domain;
using Infrastructure;

namespace Application
{
   public class DtoMapper : IDtoMapper
   {
      private readonly IResourceLinkProvider m_linkProvider;

      #region Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Object"/> class.
      /// </summary>
      public DtoMapper(IResourceLinkProvider a_linkProvider)
      {
         if (a_linkProvider == null)
         {
            throw new ArgumentNullException("a_linkProvider");
         }

         m_linkProvider = a_linkProvider;
         ConfigureMapper();
      }

      #endregion

      #region Protected methods

      protected virtual void ConfigureMapper()
      {
         Mapper.Initialize(a_cfg =>
                              {
                                 a_cfg.CreateMap<Order, OrderDto>(MemberList.Source)
                                    .AfterMap((a_src, a_dest) =>
                                                 {
                                                    foreach (ILink link in m_linkProvider.GetLinks(a_src))
                                                    {
                                                       a_dest.Links.Add(link);
                                                    }
                                                 });
                                 a_cfg.CreateMap<OrderItem, OrderItemDto>(MemberList.Source);
                                 a_cfg.CreateMap<Payment, PaymentDto>(MemberList.Source);
                              });
      }

      #endregion

      #region IDtoMapper Members

      /// <summary>
      /// Maps from Domain type to Dto representation
      /// </summary>
      /// <typeparam name="TSource">Domain type</typeparam>
      /// <typeparam name="TSourceDto">DTO representation</typeparam>
      /// <param name="a_source">Domain instance</param>
      /// <returns>Dto representation</returns>
      public TSourceDto Map<TSource, TSourceDto>(TSource a_source)
      {
         return Mapper.Map<TSource, TSourceDto>(a_source);
      }

      #endregion
   }
}