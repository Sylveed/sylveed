namespace Sylveed.SampleApp.Sample.Presentation.Devices
{
    public interface IDeviceView
    {
        void UpdateName(string name);
        void UpdateActive(bool active);
        void Focus();
    }
}