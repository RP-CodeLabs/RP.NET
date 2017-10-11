namespace RP.Net.Data.Entities
{
    public class CustomerEntity : Entity
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        public string EmploymentStatus { get; set; }

        public string Occupation { get; set; }

        public PostalAddressEntity PostalAddress { get; set; }

        public string EmailAddress { get; set; }

        public string HomeNumber { get; set; }

        public string MobileNumber { get; set; }
    }
}