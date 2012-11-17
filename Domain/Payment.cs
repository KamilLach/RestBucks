using Domain.BaseClass;

namespace Domain
{
   public class Payment : EntityBase
   {
      public virtual string CreditCardNumber { get; set; }
      public virtual string CardOwner { get; set; }
   }
}