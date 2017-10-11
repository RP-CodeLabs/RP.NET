using System;
using RP.Net.Common;

namespace RP.Net.Web.ValueObjects
{
    public class CustomerDateOfBirth : ValueObject<CustomerDateOfBirth>
    {
        public CustomerDateOfBirth(string value)
        {
            Value = value;
        }

        public string Value { get;  }

        public static explicit operator CustomerDateOfBirth(string dateOfBirth) => Create(dateOfBirth).Value;

        public static implicit operator string(CustomerDateOfBirth customerDateOfBirth) => customerDateOfBirth.Value;

        public static Result<CustomerDateOfBirth> Create(Maybe<string> dateOfBirth, string format = "dd/MM/yyyy")
            => dateOfBirth.ToResult()
                .AddErrorParameters(nameof(dateOfBirth), dateOfBirth)
                .Ensure(() => !string.IsNullOrEmpty(format))
                .AddErrorParameters(nameof(format), format)
                .OnSuccess(DateTime.Parse)
                .OnFailure(Log)
                .Map(dob => new CustomerDateOfBirth(dob.ToString(format)));

        protected override bool EqualsCore(CustomerDateOfBirth other) => Value == other.Value;

        protected override int GetHashCodeCore() => Value.GetHashCode();

        private static void Log(Result result)
        {
            if (result.IsFailure)
            {
                // log error
            }
        }
    }
}