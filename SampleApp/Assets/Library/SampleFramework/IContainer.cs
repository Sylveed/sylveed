namespace Sylveed.SampleApp.Sample.Library.SampleFramework
{
    public interface IContainer
    {
        T Resolve<T>();
    }
}