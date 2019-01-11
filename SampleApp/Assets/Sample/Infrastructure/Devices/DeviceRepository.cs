using System;
using System.Collections.Generic;
using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Library.AsDevices;
using Sylveed.SampleApp.Sample.Library.MkDevices;

namespace Sylveed.SampleApp.Sample.Infrastructure.Devices
{
    public class DeviceRepository : IDeviceRepository
    {
        readonly AsDeviceManager asDeviceManager;
        readonly MkDeviceManager mkDeviceManager;

        readonly Dictionary<DeviceId, Device> map = new Dictionary<DeviceId, Device>();

        readonly HashSet<DeviceId> transactionalDeviceIds =
            new HashSet<DeviceId>();

        readonly Dictionary<DeviceId, DeviceChange> bufferedDeviceChanges =
            new Dictionary<DeviceId, DeviceChange>();

        public Device Find(DeviceId id)
        {
            return map.TryGetValue(id, out var device) ? device : null;
        }

        public void BeginTransaction(DeviceId id)
        {
            transactionalDeviceIds.Add(id);
        }

        public void EndTransaction(DeviceId id)
        {
            transactionalDeviceIds.Remove(id);
        }

        public void SaveChange(DeviceId id, DeviceChange change)
        {
            if (transactionalDeviceIds.Contains(id))
            {
                bufferedDeviceChanges.Add(id, change);
            }
            else
            {
                Save(id, change);
            }
        }

        public void CommitChange(DeviceId id)
        {
            if (bufferedDeviceChanges.TryGetValue(id, out var change))
            {
                Save(id, change);

                bufferedDeviceChanges.Remove(id);
            }
        }

        public void DisposeChange(DeviceId id)
        {
            bufferedDeviceChanges.Remove(id);
        }

        void Save(DeviceId id, DeviceChange change)
        {
            throw new NotImplementedException();
        }
        
        #if false
        public void Save(Device device)
        {
            if (map.ContainsKey(device.Id))
            {
                UpdateDevice(device);
            }
            else
            {
                map[device.Id] = device;

                UpdateDevice(device);
            }
        }

        void UpdateDevice(Device device)
        {
            switch (device.DeviceKind)
            {
                case DeviceKind.As:
                    UpdateAsDevice(device);
                    break;
                case DeviceKind.Mk:
                    UpdateMkDevice(device);
                    break;
            }
        }

        void UpdateAsDevice(Device device)
        {
            var source = asDeviceManager.GetDevice(device.Id.ToRawDeviceId());
            
            void UpdateProperty<T>(AsPropertyKeys key, T value)
            {
                source.Properties[key] = value;
            }

            T GetProperty<T>(AsPropertyKeys key)
            {
                return (T)source.Properties[key];
            }
            
            UpdateProperty(AsPropertyKeys.Name, device.Name);

            var status = GetProperty<AsDeviceStatus>(AsPropertyKeys.Status);
            status.IsActive = device.IsActive;
            UpdateProperty(AsPropertyKeys.Status, status);
        }

        void UpdateMkDevice(Device device)
        {
            var source = mkDeviceManager.GetDevice(device.Id.ToRawDeviceId());
            
            void UpdateProperty<T>(MkPropertyKeys key, T value)
            {
                source.Properties[key] = value;
            }

            UpdateProperty(MkPropertyKeys.Name, device.Name);
            UpdateProperty(MkPropertyKeys.IsActive, device.IsActive);
        }
    #endif
    }
}