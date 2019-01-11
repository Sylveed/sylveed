using Sylveed.SampleApp.Sample.Application.Devices;
using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Presentation.Devices;

namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class DevicePropertyPresenter : IDevicePropertyPresenter
    {
        readonly DeviceViewRepository deviceViewRepository;
        readonly IMonitorViewHolder viewHolder;
        
        public void UpdateActive(DeviceId id, bool isActive)
        {
            var device = deviceViewRepository.Find(id);

            device.UpdateActive(isActive);

            var property = viewHolder.DevicePropertyView;

            if (property.Visible)
            {
                property.UpdateActive(isActive);
            }
        }

        public void UpdateName(DeviceId id, string newName)
        {
            var deviceView = deviceViewRepository.Find(id);

            deviceView.UpdateName(newName);

            var property = viewHolder.DevicePropertyView;

            if (property.Visible)
            {
                property.UpdateName(newName);
            }
        }
    }
}