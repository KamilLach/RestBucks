using System.Collections.Generic;
using Application.Dto;
using AutoMapper;
using Domain;
using Infrastructure;

namespace Application
{
   public class DtoLinkMapperResolver : ValueResolver<Order, List<Link>>
   {
      #region Overrides of ValueResolver<Order,List<Link>>

      protected override List<Link> ResolveCore(Order a_source)
      {
         throw new System.NotImplementedException();
      }

      #endregion
   }
}