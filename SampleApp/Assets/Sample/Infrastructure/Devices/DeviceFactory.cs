using System;
using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Library.AsDevices;
using Sylveed.SampleApp.Sample.Library.MkDevices;
using AsPropertyKeys = Sylveed.SampleApp.Sample.Library.AsDevices.PropertyKeys;
using MkPropertyKeys = Sylveed.SampleApp.Sample.Library.MkDevices.PropertyKeys;

namespace Sylveed.SampleApp.Sample.Infrastructure.Devices
{
    public class DeviceFactory
    {
        readonly AsDeviceManager asDeviceManager;
        readonly MkDeviceManager mkDeviceManager;

        public Device Create(DeviceKind kind, string name)
        {
            switch (kind)
            {
                case DeviceKind.As:
                {
                    var source = asDeviceManager.CreateDevice();
                    source.Properties[AsPropertyKeys.Name] = name;

                    var id = new DeviceId(source.DeviceId);

                    return new Device(id, kind, name);
                }

                case DeviceKind.Mk:
                {
                    var source = mkDeviceManager.CreateDevice();
                    source.Properties[MkPropertyKeys.Name] = name;

                    var id = new DeviceId(source.DeviceId);

                    return new Device(id, kind, name);
                }

                default:
                    throw new InvalidOperationException();
            }
        }
    }
}