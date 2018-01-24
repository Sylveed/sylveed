using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive
{
    public class BooleanDisposable : IDisposable
    {
        public bool IsDisposed { get; private set; }

        public void Dispose()
        {
            IsDisposed = true;
        }
    }
}