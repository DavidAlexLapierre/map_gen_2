using Engine.Core;
using Microsoft.Xna.Framework;

namespace Engine.Components {
    class PositionComponent : Component {
        public Vector2 Coords { get; private set; }
        public Vector2 Direction { get; private set; }
        
        public PositionComponent() {}

        public void SetCoords(float x, float y) {
            Coords = new Vector2(x, y);
        }

        public void Move(Vector2 direction) {
            Coords += direction;
            SetDirection(direction);
        }

        void SetDirection(Vector2 dir) {
            Direction = Vector2.Normalize(dir);
        }

        public void Bottom(int totalH) {
            var dim = Parent.GetComponent<DimensionComponent>();
            if (dim != null) {
                var yy = totalH - dim.Height;
                SetCoords(Coords.X, yy);
            }
        }

        public void Right(int totalW) {
            var dim = Parent.GetComponent<DimensionComponent>();
            if (dim != null) {
                var xx = totalW - dim.Width;
                SetCoords(xx, Coords.Y);
            }
        }

        public void Left() {
            SetCoords(0, Coords.Y);
        }

        public void Top() {
            SetCoords(Coords.X, 0);
        }

        public void Center(int totalW, int totalH) {
            CenterHorizontally(totalW);
            CenterVertically(totalH);
        }

        public void CenterHorizontally(int totalW) {
            var dim = Parent.GetComponent<DimensionComponent>();
            if (dim != null) {
                var halfW = dim.Width / 2.0f;
                var center = totalW / 2.0f;
                var xx = center - halfW;
                SetCoords(xx, Coords.Y);
            }
        }

        public void CenterVertically(int totalH) {
            var dim = Parent.GetComponent<DimensionComponent>();
            if (dim != null) {
                var halfH = dim.Height / 2.0f;
                var center = totalH / 2.0f;
                var yy = center - halfH;
                SetCoords(Coords.X, yy);
            }
        }
    }
}