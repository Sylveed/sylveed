
namespace Sylveed.SampleApp.Sample.Domain.Devices
{
    public interface IDeviceRepository
    {
        Device Find(DeviceId id);
    }
}