using System.Collections.Generic;

namespace Domain.BaseClass
{
   public interface IValidable
   {
      IEnumerable<string> GetErrorMessages();
   }
}