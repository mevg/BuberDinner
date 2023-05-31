using BuberDinner.Domain.Common.Models;

namespace BuberDinner.Domain.Common.ValueObjects
{
    public sealed class Rating : ValueObject
    {
        public int Value { get; set; }

        private Rating(int value)
        {
            Value = value;
        }

        public static Rating Create(int value)
        {
            return new(value);
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}