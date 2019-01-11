namespace Sylveed.SampleApp.Sample.Domain.Devices
{
    public class DeviceId
    {
        readonly string value;

        public DeviceId(string value)
        {
            this.value = value;
        }

        public string ToRawDeviceId()
        {
            return value;
        }

        public static DeviceId Of(string value)
        {
            return new DeviceId(value);
        }
    }
}