using Sylveed.SampleApp.Sample.Domain.Devices;

namespace Sylveed.SampleApp.Sample.Domain.Users
{
    public interface IUserRepository
    {
        User Find(UserId id);
        User CurrentUser();
        Device FindOwnedDevice(UserId userId);
    }
}