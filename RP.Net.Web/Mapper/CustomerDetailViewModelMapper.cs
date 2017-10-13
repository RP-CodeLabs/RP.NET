using System.Collections.Generic;
using RP.Net.Common;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Logging;
using RP.Net.Web.Models;
using RP.Net.Web.ValueObjects;

namespace RP.Net.Web.Mapper
{
    public class CustomerDetailViewModelMapper
    {
        private readonly ILogger _logger;

        public CustomerDetailViewModelMapper(ILogger logger)
        {
            _logger = logger;
        }

        public CustomerDetailsViewModel Map(Maybe<CustomerDetailsViewModel> viewModel,
            Maybe<ServiceResponse<CustomerEntity>> response)
        {
            viewModel.ToResult()
                     .AddErrorParameters(nameof(viewModel), viewModel)
                     .Ensure(() => response.HasValue && response.Value.IsSuccess)
                     .AddErrorParameters(nameof(response), response)
                     .Ensure(() => response.HasValue  && response.Value.Enity != null)
                     .AddErrorParameters(nameof(response.Value), response.Value?.Enity)
                     .OnBoth(model => MapCustomer(response.Value?.Enity, viewModel?.Value))
                     .OnFailure(result => _logger.OnArgumentNullOrEmpty(result.Error));
            return viewModel.Value;
        }

        private Result MapCustomer(Maybe<CustomerEntity> customer, CustomerDetailsViewModel viewModel)
        {
            if (customer.HasNoValue)
            {
                return Result.Fail(new Dictionary<string, object>() {{nameof(customer.Value), customer.Value}});
            }
            viewModel.FullName = PersonName.Create(customer).Value;
            viewModel.DateOfBirth = CustomerDateOfBirth.Create(customer.Value.DateOfBirth, viewModel.Configuration?.Format).Value;
            viewModel.Address = PostalAddress.Create(customer.Value.PostalAddress).Value;
            viewModel.MobileNumber = customer.Value.MobileNumber;
            viewModel.HomeNumber = customer.Value.HomeNumber;
            viewModel.EmailAddress = customer.Value.EmailAddress;
            return Result.Ok();
        }
    }
}