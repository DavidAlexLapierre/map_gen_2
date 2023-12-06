using Engine.Components;
using Engine.Events;
using Microsoft.Xna.Framework;

namespace Engine.Core {
    abstract class Camera : Entity {
        public Camera(Game game, SceneEvents events) : base(game, events) {
        }

        public abstract bool GetVisibility(PositionComponent entityPos);

        public override void Discard() {
            _game.Services.RemoveService(typeof(Camera));
            base.Discard();
        }
    }
}