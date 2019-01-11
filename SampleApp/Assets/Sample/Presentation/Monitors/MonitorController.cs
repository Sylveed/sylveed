using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Presentation.Helpers;
using Sylveed.SampleApp.Sample.UseCase.Monitors;

namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class MonitorController
    {
        readonly FocusObject focusObject;

        public void FocusDevice(string deviceId)
        {
            ThreadDispatcher.Dispatch(
                focusObject.Focus,
                DeviceId.Of(deviceId));
        }
    }
}