using Engine.Core;
using Microsoft.Xna.Framework;

namespace Engine.Components {
    class ScaleComponent : Component {
        public Vector2 Scale { get; private set; }
        public ScaleComponent() {
            Scale = Vector2.One;
        }

        public void SetScale(Vector2 scale) {
            Scale = scale;
            if (Parent != null) {
                var pos = Parent.GetComponent<PositionComponent>();
                if (pos != null) {
                    pos.SetCoords(pos.Coords.X * scale.X, pos.Coords.Y * scale.Y);
                }
            }
        }
    }
}