using NUnit.Framework;
using RP.Net.Common;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Logging;
using RP.Net.Web.Mapper;
using RP.Net.Web.Models;

namespace RP.Net.Tests
{
    [TestFixture]
    public class CustomerDetailsViewModelMapperTests
    {
        private CustomerDetailViewModelMapper _mapper;
        private Maybe<CustomerDetailsViewModel> _viewModel;
        private Maybe<ServiceResponse<CustomerEntity>> _serviceResponse;

        [OneTimeSetUp]
        public void Setup()
        {
            var logger = new Logger();
            _mapper = new CustomerDetailViewModelMapper(logger);
            _viewModel = new CustomerDetailsViewModel();
            _serviceResponse = new ServiceResponse<CustomerEntity>(FakeCustomerDetails.CustomerDetails())
            {
                IsSuccess = true
            };
        }

        [Test]
        public void map_customer_details_from_service_response_to_customerdetailsviewmodel()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void map_customer_details_from_service_response_to_customerviewmodel_has_fullname()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.FullName, Is.EqualTo("Mr Test Customer"));
        }

        [Test]
        public void map_customer_details_from_service_response_to_customerviewmodel_has_dateofbirth()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.DateOfBirth, Is.EqualTo("01/01/0001 00:00:00"));
        }

        [Test]
        public void map_customer_details_from_service_response_to_customerviewmodel_has_postaladdress()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.Address, Is.EqualTo("Some Avenue, XA11XA"));
        }

        [Test]
        public void map_customer_details_from_service_response_to_customerviewmodel_has_mobilenumber()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.MobileNumber, Is.EqualTo("0123456789"));
        }

        [Test]
        public void map_customer_details_response_to_customerviewmodel_has_house_number()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.HomeNumber, Is.EqualTo("01"));
        }

        [Test]
        public void map_customer_details_response_to_customerviewmodel_has_emailaddress()
        {
            var result = _mapper.Map(_viewModel, _serviceResponse);
            Assert.That(result.EmailAddress, Is.EqualTo("test@test.com"));
        }

        [Test]
        public void map_customer_details_response_as_null_to_customerviewmodel()
        {
            var result = _mapper.Map(_viewModel, Maybe<ServiceResponse<CustomerEntity>>.None);
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void map_customer_details_response_to_customerviewmodel_but_return_null_if_viewmodel_is_null()
        {
            var result = _mapper.Map(Maybe<CustomerDetailsViewModel>.None, Maybe<ServiceResponse<CustomerEntity>>.None);
            Assert.That(result, Is.Null);
        }
    }
}
