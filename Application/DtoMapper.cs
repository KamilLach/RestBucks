using Application.Dto;
using AutoMapper;
using Domain;
using Infrastructure;

namespace Application
{
   public class DtoMapper : IDtoMapper
   {
      #region Constructors

      /// <summary>
      /// Initializes a new instance of the <see cref="T:System.Object"/> class.
      /// </summary>
      public DtoMapper()
      {
         ConfigureMapper();
      }

      #endregion

      #region Protected methods

      protected virtual void ConfigureMapper()
      {
         Mapper.Initialize(a_cfg =>
                              {
                                 a_cfg.CreateMap<Order, OrderDto>(MemberList.Source)
                                    .ForMember(a_src => a_src.Links, a_expr => a_expr.ResolveUsing<DtoLinkMapperResolver>());
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