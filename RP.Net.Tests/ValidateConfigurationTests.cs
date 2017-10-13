using NUnit.Framework;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Builders;
using RP.Net.Web.Builders.Common;
using RP.Net.Web.Configuartion;
using RP.Net.Web.Logging;
using RP.Net.Web.Mapper;
using RP.Net.Web.Models;

namespace RP.Net.Tests
{
    [TestFixture]
    public class ValidateConfigurationTests
    {
        private CustomerDetailConfiguration _configuration;
        private ValidateBuilderConfiguration<CustomerDetailConfiguration, CustomerDetailsViewModel> _validateBuilderConfiguration;

        [OneTimeSetUp]
        public void Setup()
        {
            var logger = new Logger();
            var mapper = new CustomerDetailViewModelMapper(logger);
            var serviceResponse = new ServiceResponse<CustomerEntity>(FakeCustomerDetails.CustomerDetails())
            {
                IsSuccess = true
            };
            _configuration = new CustomerDetailConfiguration()
            {
                Format = "dd/MM/yyyy"
            };
            var viewModelBuilder = new CustomerDetailViewModelBuilder(serviceResponse, mapper);
            _validateBuilderConfiguration = new ValidateBuilderConfiguration<CustomerDetailConfiguration, CustomerDetailsViewModel>(viewModelBuilder, logger);
        }

        [Test]
        public void build_will_check_configuataion_and_log_error_and_pass_it_to_decorator()
        {
            var result = _validateBuilderConfiguration.Build(_configuration);
            Assert.That(result.FullName, Is.EqualTo("Mr Test Customer"));
        }

        [Test]
        public void build_with_configuration_as_null_should_return_viewmodel()
        {
            var result = _validateBuilderConfiguration.Build(null);
            Assert.That(result, Is.Not.Null);
        }
    }
}
