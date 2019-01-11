namespace Sylveed.SampleApp.Sample.Domain.Devices
{
    public interface IDeviceEventPublisher
    {
        void NameChanged(DeviceId id, string newName);
        void Activated(DeviceId id);
        void Deactivated(DeviceId id);
    }
}