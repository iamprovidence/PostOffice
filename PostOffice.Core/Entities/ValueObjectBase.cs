using System.Collections.Generic;
using System.Linq;

namespace PostOffice.Core.Entities
{
	public abstract class ValueObjectBase
	{
		protected static bool EqualOperator(ValueObjectBase left, ValueObjectBase right)
		{
			if (left is null ^ right is null)
			{
				return false;
			}

			return left?.Equals(right) != false;
		}

		protected static bool NotEqualOperator(ValueObjectBase left, ValueObjectBase right)
		{
			return !(EqualOperator(left, right));
		}

		protected abstract IEnumerable<object> GetAtomicValues();

		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != GetType())
			{
				return false;
			}

			var other = (ValueObjectBase)obj;
			var thisValues = GetAtomicValues().GetEnumerator();
			var otherValues = other.GetAtomicValues().GetEnumerator();

			while (thisValues.MoveNext() && otherValues.MoveNext())
			{
				if (thisValues.Current is null ^ otherValues.Current is null)
				{
					return false;
				}

				if (thisValues.Current != null &&
					!thisValues.Current.Equals(otherValues.Current))
				{
					return false;
				}
			}

			return !thisValues.MoveNext() && !otherValues.MoveNext();
		}

		public override int GetHashCode()
		{
			return GetAtomicValues()
				.Select(x => x?.GetHashCode() ?? 0)
				.Aggregate((x, y) => x ^ y);
		}
	}
}
