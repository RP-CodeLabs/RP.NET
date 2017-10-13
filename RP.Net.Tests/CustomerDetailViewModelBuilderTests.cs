using NUnit.Framework;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Builders;
using RP.Net.Web.Configuartion;
using RP.Net.Web.Logging;
using RP.Net.Web.Mapper;

namespace RP.Net.Tests
{
    [TestFixture]
    public class CustomerDetailViewModelBuilderTests
    {
        private CustomerDetailViewModelBuilder _viewModelBuilder;
        private CustomerDetailConfiguration _configuration;

        [OneTimeSetUp]
        public void Setup()
        {
            var logger = new Logger();
            var mapper = new CustomerDetailViewModelMapper(logger);
            var serviceResponse = new ServiceResponse<CustomerEntity>(FakeCustomerDetails.CustomerDetails())
            {
                IsSuccess = true
            };
            _configuration = new CustomerDetailConfiguration();
            _viewModelBuilder = new CustomerDetailViewModelBuilder(serviceResponse, mapper);
        }

        [Test]
        public void build_method_will_set_viewmodel_from_service_and_configuration()
        {
            var result = _viewModelBuilder.Build(_configuration);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void build_method_will_set_viewmodel_mobile_number_from_service_and_configuration()
        {
            var result = _viewModelBuilder.Build(_configuration);
            Assert.That(result.MobileNumber, Is.Not.Null);
        }
    }
}
