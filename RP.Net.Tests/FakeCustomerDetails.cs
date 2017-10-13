using RP.Net.Data.Entities;

namespace RP.Net.Tests
{
    public class FakeCustomerDetails
    {
        public static CustomerEntity CustomerDetails()
        {
            return new CustomerEntity()
            {
                Title = "Mr",
                FirstName = "Test",
                LastName = "Customer",
                DateOfBirth = "01/01/0001",
                EmailAddress = "test@test.com",
                EmploymentStatus = "FullTime",
                HomeNumber = "01",
                MobileNumber = "0123456789",
                Occupation = "Software Engineer",
                PostalAddress = new PostalAddressEntity { Line1 = "Some Avenue", Postcode = "XA11XA"}
            };
        }
    }
}