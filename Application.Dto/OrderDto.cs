using System.Collections.Generic;
using System.Xml.Serialization;
using Application.Dto.Base;
using Domain;

namespace Application.Dto
{
   [XmlRoot(ElementName = "order", Namespace = "http://restbuckson.net")]
   public class OrderDto : DtoBase
   {
      public OrderDto()
      {
         Items = new List<OrderItemDto>();
      }

      [XmlElement(ElementName = "location")]
      public Location Location { get; set; }

      [XmlElement(ElementName = "cost")]
      public decimal Cost { get; set; }

      [XmlArray(ElementName = "items"), XmlArrayItem(ElementName = "item")]
      public List<OrderItemDto> Items { get; set; }

      [XmlElement(ElementName = "status")]
      public OrderStatus Status { get; set; }
   }

}