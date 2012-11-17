using System.Collections.Generic;
using System.Linq;

namespace Domain.BaseClass
{
   public interface IValidable
   {
      IEnumerable<string> GetErrorMessages();
   }

   public static class ValidatableExtensions
   {
      public static bool IsValid(this IValidable a_validable)
      {
         return !a_validable.GetErrorMessages().Any();
      }
   }
}