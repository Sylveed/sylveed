namespace Sylveed.SampleApp.Sample.Domain.DeviceCommands
{
    public interface ICommandParseService
    {
        CommandParseResponse Parse(CommandKey key);
    }
}