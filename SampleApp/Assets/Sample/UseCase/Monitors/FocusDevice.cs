using Sylveed.SampleApp.Sample.Application.Monitors;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.UseCase.Monitors
{
    public class FocusDevice
    {
        readonly IDeviceRepository deviceRepository;
        readonly IFocusPresenter focusPresenter;
        readonly IMonitorNavigator navigator;

        public void Focus(DeviceId id)
        {
            var device = deviceRepository.Find(id);

            focusPresenter.FocusDevice(id);

            navigator.ShowDeviceProperty(device);
        }

        public void KillFocus(DeviceId id)
        {
            focusPresenter.KillFocus();

            navigator.HideDeviceProperty();
        }
    }
}