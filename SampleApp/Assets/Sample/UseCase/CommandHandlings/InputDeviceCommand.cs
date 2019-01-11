using Sylveed.SampleApp.Sample.Domain.DeviceCommands;
using Sylveed.SampleApp.Sample.Domain.Users;

namespace Sylveed.SampleApp.Sample.UseCase.CommandHandlings
{
    public class InputDeviceCommand
    {
        readonly IUserRepository userRepository;
        readonly ICommandParseService commandParseService;
        readonly IDeviceActionPresenter deviceActionPresenter;

        public void Handle(CommandKey key)
        {
            var parseResponse = commandParseService.Parse(key);

            if (parseResponse.DeviceAction != null)
            {
                var currentUser = userRepository.CurrentUser();

                var device = userRepository.FindOwnedDevice(currentUser.Id);

                deviceActionPresenter.Handle(device.Id, parseResponse.DeviceAction);
            }
        }
    }
}