using Domain;

namespace Application.Dto
{
   public class OrderLocationDto
   {
      public int OrderId { get; set; }
      public Location Location { get; set; }
   }
}