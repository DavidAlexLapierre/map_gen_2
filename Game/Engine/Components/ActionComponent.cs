using System;
using Engine.Core;

namespace Engine.Components {
    class ActionComponent : Component {
        public EventHandler Action { get; private set; }
        public EventArgs Args { get; private set; }

        public ActionComponent() {
            Args = EventArgs.Empty;
        }

        public void SetAction(EventHandler action) {
            Action -= Action; 
            Action += action;
        } 
        public void SetArgs(EventArgs args) { Args = args; }

        public void Execute() {
            Action?.Invoke(Parent, Args);
        }
    }
}