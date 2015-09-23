using System;

namespace ArduinoScadaManager.Common.Infrastructure._2dControl
{
    public class Ref<T>
    {
        private readonly Func<T> _getter;
        private readonly Action<T> _setter;

        public Ref(Func<T> getter, Action<T> setter)
        {
            _getter = getter;
            _setter = setter;
        }

        public T Value
        {
            get { return _getter(); } 
            set { _setter(value); }
        }
    }
}
