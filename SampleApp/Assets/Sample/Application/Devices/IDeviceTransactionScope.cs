using System;

namespace Sylveed.SampleApp.Sample.Application.Devices
{
    public interface IDeviceTransactionScope : IDisposable
    {
        void Commit();
    }
}