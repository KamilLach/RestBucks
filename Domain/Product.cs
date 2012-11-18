using System.Collections.Generic;
using Domain.BaseClass;

namespace Domain
{
   public class Product : EntityBase
   {
      private readonly ICollection<Customization> _customizations;

      public Product()
      {
         _customizations = new HashSet<Customization>();
      }

      public virtual string Name { get; set; }
      public virtual decimal Price { get; set; }

      public virtual ICollection<Customization> Customizations
      {
         get { return _customizations; }
      }
   }
}