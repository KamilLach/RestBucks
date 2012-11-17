namespace Domain.BaseClass
{
   public class EntityBase : IVersionable
   {
      public virtual long Id { get; set; }

      #region IVersionable Members

      public virtual int Version { get; set; }

      #endregion

      public override bool Equals(object a_obj)
      {
         var other = a_obj as EntityBase;
         if (other == null) return false;
         if (default(long) == other.Id) return ReferenceEquals(this, a_obj);
         return other.Id == Id;
      }

      public override int GetHashCode()
      {
         return Id.GetHashCode();
      }
   }
}