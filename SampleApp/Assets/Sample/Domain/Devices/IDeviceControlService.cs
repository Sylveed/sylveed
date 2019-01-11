namespace Sylveed.SampleApp.Sample.Domain.Devices
{
    public interface IDeviceControlService : IDeviceEventPublisher
    {
        void Save(DeviceId deviceId);
    }
}