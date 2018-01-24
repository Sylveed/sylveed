using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Assets.Sylveed.Reactive
{
    public static class Disposable
    {
        public static readonly IDisposable Empty = new AnonymousDisposable(null);

        public static IDisposable Create(Action dispose)
        {
            return new AnonymousDisposable(dispose);
        }

        class AnonymousDisposable : IDisposable
        {
            readonly Action dispose;
            bool isDisposed = false;

            public AnonymousDisposable(Action dispose)
            {
                this.dispose = dispose;
            }

            public void Dispose()
            {
                if (!isDisposed)
                {
                    isDisposed = true;

                    if (dispose != null)
                        dispose();
                }
            }
        }
    }
}