using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Application.Devices
{
    public interface IDeviceTransactionService
    {
        IDeviceTransactionScope Begin(DeviceId deviceId);
    }
}