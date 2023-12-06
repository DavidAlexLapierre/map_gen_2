using System;
using Engine.Core;

namespace Engine.Components {
    class FocusComponent : Component {
        EventHandler Callback;
        public bool IsFocused { get; private set ;}

        public FocusComponent(EventHandler callback = null) {
            Callback -= Callback;
            Callback += callback;
        }

        public void ToggleFocus() {
            IsFocused = !IsFocused;
            Callback?.Invoke(this, EventArgs.Empty);
        }

        public void OverrideCallback(EventHandler handler = null) {
            Callback -= Callback;
            Callback += handler;
        }
    }
}