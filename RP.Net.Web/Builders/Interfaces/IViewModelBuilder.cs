using RP.Net.Web.Configuartion;

namespace RP.Net.Web.Builders.Interfaces
{
    public interface IViewModelBuilder<in TConfiguration, out TViewModel> 
    {
        TViewModel Build(TConfiguration configuration);
    }
}