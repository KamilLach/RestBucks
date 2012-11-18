using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.Persistance.Mappings
{
   public class OrderItemMapper : ClassMap<OrderItem>
   {
      public OrderItemMapper()
      {
         Id(a_x => a_x.Id);
         Version(a_x => a_x.Version);
         Map(a_x => a_x.UnitPrice);
         Map(a_x => a_x.Quantity);
         HasMany(a_x => a_x.Preferences).AsMap<string>("id").Element("idx", a_x => a_x.Type<string>())
            .KeyColumn("orderitem_key").Cascade.All();
         References(a_x => a_x.Order);
         References(a_x => a_x.Product);
      }
   }
}