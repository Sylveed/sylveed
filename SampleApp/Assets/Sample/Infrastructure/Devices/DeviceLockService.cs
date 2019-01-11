using System;
using System.Collections.Generic;
using System.Threading;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Infrastructure.Devices
{
    public class DeviceLockService
    {
        readonly object gate = new object();
        
        readonly Dictionary<DeviceId, AutoResetEvent> deviceLocks = new Dictionary<DeviceId, AutoResetEvent>();

        readonly LockHandlePool deviceLockPool = new LockHandlePool();

        public void LockDevice(DeviceId id)
        {
            lock (gate)
            {
                deviceLocks.Add(id, deviceLockPool.Get());
            }
        }

        public void UnlockDevice(DeviceId id)
        {
            lock (gate)
            {
                if (deviceLocks.TryGetValue(id, out var handle))
                {
                    handle.Set();

                    deviceLockPool.Return(handle);

                    deviceLocks.Remove(id);
                }
            }
        }

        public void WaitIfNeeded(DeviceId id)
        {
            lock (gate)
            {
                if (deviceLocks.TryGetValue(id, out var handle))
                {
                    handle.WaitOne();
                }
            }
        }

        class LockHandlePool
        {
            public AutoResetEvent Get()
            {
                throw new NotImplementedException();
            }

            public void Return(AutoResetEvent handle)
            {
                throw new NotImplementedException();
            }
        }
    }
}