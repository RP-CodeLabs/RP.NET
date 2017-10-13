using System.Collections.Generic;
using RP.Net.Web.Builders.Interfaces;
using RP.Net.Web.Logging;

namespace RP.Net.Web.Builders.Common
{
    public class ValidateBuilderConfiguration<TConfiguration, TViewModel> : IViewModelBuilder<TConfiguration, TViewModel> 
    {
        private readonly IViewModelBuilder<TConfiguration, TViewModel> _viewModelBuilder;
        private readonly ILogger _logger;

        public ValidateBuilderConfiguration(IViewModelBuilder<TConfiguration, TViewModel> decorator, ILogger logger)
        {
            _logger = logger;
            _viewModelBuilder = decorator;
        }

        public TViewModel Build(TConfiguration configuration)
        {
            if (configuration == null)
            {
                _logger.OnArgumentNullOrEmpty(new Dictionary<string,object> { {nameof(configuration), null} });
            }
            return _viewModelBuilder.Build(configuration);
        }
    }
}