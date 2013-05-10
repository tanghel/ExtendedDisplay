using System;

namespace ExtendedDisplay
{
    public delegate void GenericDataEventHandler<T>(object sender, GenericDataEventArgs<T> args);

    public class GenericDataEventArgs<T> : EventArgs
    {
        public T Value { get; private set; }

        public GenericDataEventArgs(T value)
        {
            this.Value = value;
        }
    }
}

