using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UniRx;

namespace Assets.Sylveed.DynamicModel
{
    public class EventAttribute : Attribute
    {
        public EventAttribute()
        {

        }
        public EventAttribute(string name)
        {

        }
        public EventAttribute(Func<int, string> func)
        {

        }
    }

    public class ReactiveAttribute : Attribute
    {
    }

    public abstract class TestClass
    {
        [Reactive]
        public int TestValue { get; set; }

        [Event(nameof(TestValue))]
        public abstract IObservable<int> TestValueChanged { get; }

        void Calc()
        {

        }

        void Sum()
        {

        }
    }

    static class Main
    {
        static Main()
        {

        }
    }
}