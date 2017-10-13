using System.Collections.Generic;
using RP.Net.Data.Entities;
using RP.Net.Data.Service;
using RP.Net.Web.Builders.Interfaces;
using RP.Net.Web.Configuartion;
using RP.Net.Web.Logging;
using RP.Net.Web.Mapper;
using RP.Net.Web.Models;

namespace RP.Net.Web.Builders
{
    public class CustomerDetailViewModelBuilder1 : IViewModelBuilder<CustomerDetailConfiguration, CustomerDetailsViewModel>
    {
        private readonly ILogger _logger;
        private readonly ServiceResponse<CustomerEntity> _serviceResponse;
        private readonly CustomerDetailViewModelMapper _viewModelMapper;

        public CustomerDetailViewModelBuilder1(ServiceResponse<CustomerEntity> serviceResponse, CustomerDetailViewModelMapper mapper, ILogger logger)
        {
            _viewModelMapper = mapper;
            _serviceResponse = serviceResponse;
            _logger = logger;
        }

        public CustomerDetailsViewModel Build(CustomerDetailConfiguration configuration)
        {
            if (configuration == null)
            {
                _logger.OnArgumentNullOrEmpty(new Dictionary<string, object>{ { nameof(configuration), null } });
            }
            var viewModel = new CustomerDetailsViewModel();
            viewModel = _viewModelMapper.Map(viewModel, _serviceResponse);
            return viewModel;
        }
    }
}