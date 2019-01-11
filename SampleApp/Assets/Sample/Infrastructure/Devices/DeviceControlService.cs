using System;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Infrastructure.Devices
{
    public class DeviceControlService : IDeviceControlService
    {
        readonly object gate = new object();

        readonly DeviceRepository deviceRepository;
        readonly DeviceLockService lockService;

        public void Activated(DeviceId id)
        {
            lockService.WaitIfNeeded(id);

            throw new NotImplementedException();
        }

        public void Deactivated(DeviceId id)
        {
            lockService.WaitIfNeeded(id);
            
            throw new NotImplementedException();
        }

        public void NameChanged(DeviceId id, string newName)
        {
            lockService.WaitIfNeeded(id);
            
            throw new NotImplementedException();
        }

        public void Save(DeviceId deviceId)
        {
            throw new System.NotImplementedException();
        }
    }
}