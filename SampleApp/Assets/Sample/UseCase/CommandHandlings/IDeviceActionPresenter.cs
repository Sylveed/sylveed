using Sylveed.SampleApp.Sample.Domain.DeviceCommands;
using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.UseCase.CommandHandlings
{
    public interface IDeviceActionPresenter
    {
        void Handle(DeviceId deviceId, DeviceAction deviceAction);
    }
}