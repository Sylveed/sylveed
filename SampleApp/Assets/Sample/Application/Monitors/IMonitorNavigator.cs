using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Application.Monitors
{
    public interface IMonitorNavigator
    {
        void ShowDeviceProperty(Device device);
        void HideDeviceProperty();
    }
}