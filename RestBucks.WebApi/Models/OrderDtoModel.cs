using Application.Dto;

namespace RestBucks.WebApi.Models
{
   public class OrderDtoModel
   {
      public int OrderId { get; set; }
      public OrderDto OrderDto { get; set; }
   }
}