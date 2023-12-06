using System;

namespace Engine.Events {
    class SceneEventArgs : EventArgs {
        public object Param_1 { get; private set; }
        public object Return_value { get; set; }

        public SceneEventArgs(object param_1 = null) : base() {
            Param_1 = param_1;
        }
    }
}
