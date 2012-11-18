using System.Collections.Generic;
using Domain.BaseClass;

namespace Domain
{
    public class Customization : EntityBase
    {
       protected readonly ICollection<string> _possibleValues;
        
        public Customization()
        {
            _possibleValues = new HashSet<string>();
        }

        public virtual string Name { get; set; }

        public virtual ICollection<string> PossibleValues
        {
           get { return _possibleValues; }
        }
    }
}