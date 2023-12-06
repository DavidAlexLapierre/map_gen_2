using System;

namespace Engine.Events {
    class GenericEventArgs : EventArgs {
        public object Param_1 { get; private set; }
        public object Param_2 { get; private set; }
        public object Param_3 { get; private set; }

        public GenericEventArgs(object param_1 = null, object param_2 = null, object param_3 = null) : base() {
            Param_1 = param_1;
            Param_2 = param_2;
            Param_3 = param_3;
        }
    }
}
