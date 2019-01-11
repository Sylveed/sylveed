namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public interface IDevicePropertyView
    {
        bool Visible { get; }
        void Show(string deviceId);
        void Hide();
        void UpdateName(string value);
        void UpdateActive(bool value);
    }
}