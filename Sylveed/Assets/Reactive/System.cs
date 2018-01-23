using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace Sylveed.Reactive
{
    public struct Unit
    {
        public static readonly Unit Default = new Unit();
    }

    public static class Stubs
    {
        public static Action Nop = () => { };
        public static Action<Exception> Throw = ex => { throw ex; };
    }

    public static class Stubs<T>
    {
        public static Action<T> Nop = _ => { };
    }
}