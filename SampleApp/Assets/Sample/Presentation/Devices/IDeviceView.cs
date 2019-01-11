namespace Sylveed.SampleApp.Sample.Presentation.Devices
{
    public interface IDeviceView
    {
        string Id { get; }

        void UpdateName(string name);
        void UpdateActive(bool active);
        void ResetStatus();
    }
}