using System.Collections.Generic;
using System.Xml.Serialization;
using Application.Dto.Base;
using Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Application.Dto
{
   public class OrderDto : DtoBase
   {
      #region Fields

      [JsonConverter(typeof (StringEnumConverter))]
      public Location Location { get; set; }

      public decimal Cost { get; set; }

      public List<OrderItemDto> Items { get; set; }

      [JsonConverter(typeof (StringEnumConverter))]
      public OrderStatus Status { get; set; }

      #endregion

      #region Constructors

      public OrderDto()
      {
         Items = new List<OrderItemDto>();
      }

      #endregion
   }
}