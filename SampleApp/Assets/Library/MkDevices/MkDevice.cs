using System.Collections.Generic;

namespace Sylveed.SampleApp.Sample.Library.MkDevices
{
    public class MkDevice
    {
        public string DeviceId { get; }
        public IDictionary<PropertyKeys, object> Properties { get; }
    }
}