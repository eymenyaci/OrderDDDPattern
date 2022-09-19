using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.SeedWork
{
    public abstract class ValueObject
    {
        //returns false if left and right are not the same.
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            //returns true if left value is null or left and right are equal.
            return ReferenceEquals(left, null) || left.Equals(right);
        }

        #region Logical exclusive or operator
        //Console.WriteLine(true ^ true);    // output: False
        //Console.WriteLine(true ^ false);   // output: True
        //Console.WriteLine(false ^ true);   // output: True
        //Console.WriteLine(false ^ false);  // output: False
        #endregion

        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        protected abstract IEnumerable<Object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }
            
            var other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }
    }
}
