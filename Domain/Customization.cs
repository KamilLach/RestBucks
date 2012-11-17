using System.Collections.Generic;
using Domain.BaseClass;

namespace Domain
{
    public class Customization : EntityBase
    {
        private readonly ISet<string> m_possibleValues;
        
        public Customization()
        {
            m_possibleValues = new HashSet<string>();
        }

        public virtual string Name { get; set; }

        public virtual ISet<string> PossibleValues
        {
            get { return m_possibleValues; }
        }
    }
}