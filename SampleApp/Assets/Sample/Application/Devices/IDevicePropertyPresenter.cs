using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Application.Devices
{
    public interface IDevicePropertyPresenter
    {
        void UpdateName(DeviceId id, string newName);
        void UpdateActive(DeviceId id, bool isActive);
        void Reset(DeviceId id);
    }
}