using System;
using System.Collections.Generic;
using System.Text;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Logging;
using RP.Net.Web.Models;

namespace RP.Net.Web.Mapper
{
    public class CustomerDetailsViewModelMapper
    {
        private readonly ILogger _logger;
        public CustomerDetailsViewModelMapper(ILogger logger)
        {
            _logger = logger;
        }

        public CustomerDetailsViewModel Map(CustomerDetailsViewModel viewModel, ServiceResponse<CustomerEntity> serviceResponse)
        {
            if (serviceResponse is null || serviceResponse.Enity is null || viewModel is null) 
            {
                var errors = new Dictionary<string, object>
                {
                    { nameof(serviceResponse), serviceResponse},
                    { nameof(serviceResponse.Enity), serviceResponse?.Enity },
                    { nameof(viewModel), viewModel }
                };
                _logger.OnArgumentNullOrEmpty(errors);
                return viewModel;
            }
            viewModel.FullName = GetFullName(serviceResponse.Enity);
            viewModel.DateOfBirth = FormatDate(serviceResponse.Enity.DateOfBirth, "dd/MM/yyyy");
            viewModel.Address = GetAddress(serviceResponse.Enity.PostalAddress);
            viewModel.MobileNumber = serviceResponse.Enity.MobileNumber;
            viewModel.EmailAddress = serviceResponse.Enity.EmailAddress;
            viewModel.HomeNumber = serviceResponse.Enity.HomeNumber;
            return viewModel;
        }

        private string FormatDate(string source, string format)
        {
            try
            {
                return DateTime.TryParse(source, out var dateTime) && !string.IsNullOrEmpty(format)
                    ? dateTime.ToString(format)
                    : source;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return source;
            }
        }

        private string GetFullName(CustomerEntity customerEntity)
            => !string.IsNullOrWhiteSpace(customerEntity.FirstName) &&
               !string.IsNullOrWhiteSpace(customerEntity.LastName) &&
               !string.IsNullOrWhiteSpace(customerEntity.Title)
                ? $"{customerEntity.Title} {customerEntity.FirstName} {customerEntity.LastName}"
                : string.Empty;

        private string GetAddress(PostalAddressEntity entity)
        {
            var builder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(entity?.Line1))
            {
                builder.Append(entity.Line1);
                builder.Append(!string.IsNullOrWhiteSpace(entity.Line2) ? $", {entity.Line2}" : string.Empty);
                builder.Append(!string.IsNullOrWhiteSpace(entity.Line3) ? $", {entity.Line3}" : string.Empty);
                builder.Append(!string.IsNullOrWhiteSpace(entity.Line4) ? $", {entity.Line4}" : string.Empty);
                builder.Append(!string.IsNullOrWhiteSpace(entity.Line5) ? $", {entity.Line5}" : string.Empty);
                builder.Append(!string.IsNullOrWhiteSpace(entity.Postcode)
                    ? $", {entity.Postcode.ToUpper()}"
                    : string.Empty);
                return builder.ToString();
            }
            return string.Empty;
        }
    }
}