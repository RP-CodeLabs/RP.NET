using RP.Net.Common;
using RP.Net.Data.Entities;

namespace RP.Net.Web.ValueObjects
{
    public class PersonName : ValueObject<PersonName>
    {
        public PersonName(string value) => Value = value;

        public string Value { get; }

        public static explicit operator PersonName(CustomerEntity customer) => Create(customer).Value;

        public static implicit operator string(PersonName personName) => personName.Value;

        public static Result<PersonName> Create(Maybe<CustomerEntity> customer)
            => customer.ToResult()
                .AddErrorParameters(nameof(customer), customer.Value)
                .Ensure(() => customer.Value.Title != string.Empty)
                .Ensure(() => customer.Value.FirstName != string.Empty)
                .Ensure(() => customer.Value.LastName != string.Empty)
                .Map(cust => new PersonName($"{cust.Title} {cust.FirstName} {cust.LastName}"));

        protected override bool EqualsCore(PersonName other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}