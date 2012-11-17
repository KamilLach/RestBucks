using System.Collections.Generic;
using Domain.BaseClass;

namespace Domain
{
   public class Product : EntityBase
   {
      private readonly ISet<Customization> m_customizations;

      public Product()
      {
         m_customizations = new HashSet<Customization>();
      }

      public virtual string Name { get; set; }
      public virtual decimal Price { get; set; }

      public virtual ISet<Customization> Customizations
      {
         get { return m_customizations; }
      }
   }
}