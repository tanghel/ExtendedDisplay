using System;

namespace ExtendedDisplay
{
    public delegate void GenericDataEventHandler<T>(object sender, GenericDataEventArgs<T> args);
    public delegate void GenericDataEventHandler<T1, T2>(object sender, GenericDataEventArgs<T1, T2> args);
    
    public class GenericDataEventArgs<T> : EventArgs
    {
        public T Value { get; private set; }
        
        public GenericDataEventArgs(T value)
        {
            this.Value = value;
        }
    }
    
    public class GenericDataEventArgs<T1, T2> : EventArgs
    {
        public T1 Value1 { get; private set; }
        public T2 Value2 { get; private set; }
        
        public GenericDataEventArgs(T1 value1, T2 value2)
        {
            this.Value1 = value1;
            this.Value2 = value2;
        }
    }
}

