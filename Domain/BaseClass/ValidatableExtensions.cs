using System.Linq;

namespace Domain.BaseClass
{
   public static class ValidatableExtensions
   {
      public static bool IsValid(this IValidable a_validable)
      {
         return !a_validable.GetErrorMessages().Any();
      }
   }
}