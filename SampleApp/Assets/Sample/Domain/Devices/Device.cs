using System;

namespace Sylveed.SampleApp.Sample.Domain.Devices
{
    public class Device : IEquatable<Device>
    {
        public DeviceId Id { get; }

        public DeviceKind DeviceKind { get; }

        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Device(
            DeviceId id,
            DeviceKind deviceKind,
            string name)
        {
            Id = id;
            DeviceKind = deviceKind;
            Name = name;
            IsActive = true;
        }

        public void ChangeName(string newName, IDeviceEventPublisher eventPublisher)
        {
            if (Name == newName)
                return;

            Name = newName;

            eventPublisher.NameChanged(Id, newName);
        }

        public void Activate(IDeviceEventPublisher eventPublisher)
        {
            if (IsActive)
                return;

            IsActive = true;

            eventPublisher.Activated(Id);
        }

        public void Deactivate(IDeviceEventPublisher eventPublisher)
        {
            if (!IsActive)
                return;

            IsActive = false;

            eventPublisher.Deactivated(Id);
        }

        public bool Equals(Device other)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}