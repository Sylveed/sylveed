using Sylveed.SampleApp.Sample.Application.Monitors;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class MonitorNavigator : IMonitorNavigator
    {
        readonly IMonitorViewHolder viewHolder;
        
        public void ShowDeviceProperty(Device device)
        {
            var view = viewHolder.DevicePropertyView;

            view.Show(device.Id.ToRawDeviceId());

            view.UpdateName(device.Name);
            view.UpdateActive(device.IsActive);
        }

        public void HideDeviceProperty()
        {
            var view = viewHolder.DevicePropertyView;
            view.Hide();
        }
    }
}