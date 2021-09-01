using System.Collections.Generic;
using System.Linq;

namespace Advertising.Domain.Values
{
    public abstract class ValueObjectBase
    {
        protected abstract IEnumerable<object?> GetEqualityComponents();

        protected static bool EqualOperator(ValueObjectBase? left, ValueObjectBase? right)
        {
            if (ReferenceEquals(left, objB: null) ^ ReferenceEquals(right, objB: null)) return false;

            return ReferenceEquals(left, objB: null) || left.Equals(right);
        }

        protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
        {
            return !(EqualOperator(left, right));
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType()) return false;

            var other = (ValueObjectBase)obj;

            return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                  .Select(x => x != null
                                   ? x.GetHashCode()
                                   : 0)
                  .Aggregate((x, y) => x ^ y);
        }
    }
}
