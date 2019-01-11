using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Presentation.Helpers;
using Sylveed.SampleApp.Sample.UseCase.DeviceControls;

namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class DevicePropertyController
    {
        readonly ChangeDeviceProperty changeDeviceProperty;

        public void ChangeName(string deviceId, string newName)
        {
            ThreadDispatcher.Dispatch(
                changeDeviceProperty.ChangeName, 
                DeviceId.Of(deviceId),
                newName);
        }

        public void Activate(string deviceId)
        {
            ThreadDispatcher.Dispatch(
                changeDeviceProperty.Activate, 
                DeviceId.Of(deviceId));
        }

        public void Deactivate(string deviceId)
        {
            ThreadDispatcher.Dispatch(
                changeDeviceProperty.Deactivate, 
                DeviceId.Of(deviceId));
        }

        public void Reset(string deviceId)
        {
            ThreadDispatcher.Dispatch(
                changeDeviceProperty.Reset, 
                DeviceId.Of(deviceId));
        }
    }
}