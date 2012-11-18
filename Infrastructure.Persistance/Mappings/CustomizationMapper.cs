using Domain;
using FluentNHibernate.Mapping;

namespace Infrastructure.Persistance.Mappings
{
   public class CustomizationMapper : ClassMap<Customization>
   {
      public CustomizationMapper()
      {
         Id(a_x => a_x.Id).GeneratedBy.Native();
         Version(a_x => a_x.Version);
         Map(a_x => a_x.Name);
         HasManyToMany(a_x => a_x.PossibleValues).Cascade.All().AsSet().Element("id").Access.CamelCaseField(Prefix.Underscore);
      }
   }
}