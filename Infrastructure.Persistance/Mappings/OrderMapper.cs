using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.Persistance.Mappings
{
   public class OrderMapper : ClassMap<Order>
   {
      public OrderMapper()
      {
         Id(a_x => a_x.Id);
         Version(a_x => a_x.Version);
         HasMany(a_x => a_x.Items).AsSet().Cascade.All().Access.CamelCaseField(Prefix.Underscore);
         References(a_x => a_x.Payment).Cascade.All().Access.BackingField();
         Map(a_x => a_x.Total).Access.ReadOnly();
         Map(a_x => a_x.Status).CustomType<int>();
         Map(a_x => a_x.Location).CustomType<int>();
         Map(a_x => a_x.CancelReason).Access.ReadOnly();
         Map(a_x => a_x.Date);
      }
   }
}