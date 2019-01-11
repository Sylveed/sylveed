using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Application.Monitors
{
    public interface IFocusPresenter
    {
        void FocusDevice(DeviceId id);
        void KillFocus();
    }
}