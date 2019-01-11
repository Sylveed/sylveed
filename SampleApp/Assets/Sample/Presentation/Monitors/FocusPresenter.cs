using Sylveed.SampleApp.Sample.Application.Monitors;
using Sylveed.SampleApp.Sample.Domain.Devices;
using Sylveed.SampleApp.Sample.Presentation.Devices;

namespace Sylveed.SampleApp.Sample.Presentation.Monitors
{
    public class FocusPresenter : IFocusPresenter
    {
        readonly DeviceViewRepository deviceViewRepository;
        readonly FocusRepository focusRepository;

        public void FocusDevice(DeviceId id)
        {
            var view = deviceViewRepository.Find(id);

            view.Focus();
        }

        public void KillFocus()
        {
            focusRepository.KillFocus();
        }
    }
}