using Sylveed.SampleApp.Sample.Application.Devices;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Presentation.Devices
{
    public class DevicePropertyPresenter : IDevicePropertyPresenter
    {
        readonly DeviceViewRepository deviceViewRepository;
        
        public void Reset(DeviceId id)
        {
            var view = deviceViewRepository.Find(id);

            view.ResetStatus();
        }

        public void UpdateActive(DeviceId id, bool isActive)
        {
            var view = deviceViewRepository.Find(id);

            view.UpdateActive(isActive);
        }

        public void UpdateName(DeviceId id, string newName)
        {
            var view = deviceViewRepository.Find(id);

            view.UpdateName(newName);
        }
    }
}