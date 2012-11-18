using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.Persistance.Mappings
{
   public class ProductMapper : ClassMap<Product>
   {
      public ProductMapper()
      {
         Id(a_x => a_x.Id);
         Version(a_x => a_x.Version);
         Map(a_x => a_x.Name);
         Map(a_x => a_x.Price);
         HasManyToMany(a_x => a_x.Customizations).AsSet().Access.CamelCaseField(Prefix.Underscore);
      }
   }
}