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
    public class CustomerDetailViewModelBuilder : IViewModelBuilder<CustomerDetailConfiguration, CustomerDetailsViewModel>
    {
        private readonly ServiceResponse<CustomerEntity> _serviceResponse;
        private readonly CustomerDetailViewModelMapper _viewModelMapper;

        public CustomerDetailViewModelBuilder(ServiceResponse<CustomerEntity> serviceResponse, CustomerDetailViewModelMapper mapper)
        {
            _viewModelMapper = mapper;
            _serviceResponse = serviceResponse;
        }

        public CustomerDetailsViewModel Build(CustomerDetailConfiguration configuration)
        {
            var viewModel = new CustomerDetailsViewModel()
            {
                Configuration = configuration
            };
            viewModel = _viewModelMapper.Map(viewModel, _serviceResponse);
            return viewModel;
        }
    }
}