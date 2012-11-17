using System.Collections.Generic;
using Domain.BaseClass;

namespace Domain
{
    public class Product : EntityBase
    {
        private readonly ISet<Customization> customizations;

        public Product()
        {
            customizations = new HashSet<Customization>();
        }

        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }

        public virtual ISet<Customization> Customizations
        {
            get { return customizations; }
        }
    }
}