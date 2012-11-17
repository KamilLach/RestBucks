namespace Infrastructure
{
   /// <summary>
   /// Interface for Dto mapper
   /// </summary>
   public interface IDtoMapper
   {
      /// <summary>
      /// Maps from Domain type to Dto representation
      /// </summary>
      /// <typeparam name="TSource">Domain type</typeparam>
      /// <typeparam name="TSourceDto">DTO representation</typeparam>
      /// <param name="a_source">Domain instance</param>
      /// <returns>Dto representation</returns>
      TSourceDto Map<TSource, TSourceDto>(TSource a_source);
   }
}