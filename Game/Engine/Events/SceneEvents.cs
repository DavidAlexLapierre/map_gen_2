using System;
using Engine.Core;

namespace Engine.Events {
    class SceneEvents {
        public Scene Scene { get; private set; }
        public EventHandler AddEntity { get; set; }
        public EventHandler RemoveEntity { get; set; }
        public EventHandler GetEntity { get; set; }
        public EventHandler TriggerNewEntities { get; set; }

        public SceneEvents(Scene scene) {
            Scene = scene;
        }
    }
}
