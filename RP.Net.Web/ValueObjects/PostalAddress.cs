using System.Text;
using RP.Net.Common;
using RP.Net.Data.Entities;

namespace RP.Net.Web.ValueObjects
{
    public class PostalAddress : ValueObject<PostalAddress>
    {
        public PostalAddress(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static explicit operator PostalAddress(PostalAddressEntity entity) => Create(entity).Value;

        public static implicit operator string(PostalAddress postalAddress) => postalAddress.Value;

        public static Result<PostalAddress> Create(Maybe<PostalAddressEntity> postalAddressEntity)
            => postalAddressEntity.ToResult()
                .AddErrorParameters(nameof(postalAddressEntity), postalAddressEntity)
                .Map(result => new PostalAddress(GetAddress(result)));

        private static string GetAddress(PostalAddressEntity entity) 
            => new StringBuilder()
            .When(() => !string.IsNullOrWhiteSpace(entity?.Line1),
            sb => sb.Append(entity.Line1)
            .Append(!string.IsNullOrWhiteSpace(entity.Line2)? $", {entity.Line2}" : string.Empty)
            .Append(!string.IsNullOrWhiteSpace(entity.Line3)? $", {entity.Line3}" : string.Empty)
            .Append(!string.IsNullOrWhiteSpace(entity.Line4)? $", {entity.Line4}" : string.Empty)
            .Append(!string.IsNullOrWhiteSpace(entity.Line5)? $", {entity.Line5}" : string.Empty)
            .Append(!string.IsNullOrWhiteSpace(entity.Postcode)? $", {entity.Postcode.ToUpper()}" : string.Empty)).ToString();

        protected override bool EqualsCore(PostalAddress other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();
    }
}