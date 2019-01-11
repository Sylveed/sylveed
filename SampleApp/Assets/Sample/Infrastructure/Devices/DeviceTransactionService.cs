using Sylveed.SampleApp.Sample.Application.Devices;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Infrastructure.Devices
{
    public class DeviceTransactionService : IDeviceTransactionService
    {
        readonly DeviceLockService lockService;
        readonly DeviceRepository deviceRepository;

        public IDeviceTransactionScope Begin(DeviceId deviceId)
        {
            lockService.LockDevice(deviceId);

            deviceRepository.BeginTransaction(deviceId);

            return new Scope(this, deviceId);
        }

        class Scope : IDeviceTransactionScope
        {
            readonly DeviceTransactionService parent;
            readonly DeviceId deviceId;

            public Scope(DeviceTransactionService parent, DeviceId deviceId)
            {
                this.parent = parent;
                this.deviceId = deviceId;
            }

            public void Commit()
            {
                parent.deviceRepository.CommitChange(deviceId);
            }

            public void Dispose()
            {
                parent.deviceRepository.DisposeChange(deviceId);

                parent.deviceRepository.EndTransaction(deviceId);

                parent.lockService.UnlockDevice(deviceId);
            }
        }
    }
}