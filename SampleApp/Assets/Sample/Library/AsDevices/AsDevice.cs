using System.Collections.Generic;

namespace Sylveed.SampleApp.Sample.Library.AsDevices
{
    public class AsDevice
    {
        public string DeviceId { get; }
        public IDictionary<PropertyKeys, object> Properties { get; }
    }
}