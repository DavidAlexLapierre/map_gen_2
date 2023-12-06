using Engine.Core;

namespace Engine.Components {
    class DimensionComponent : Component {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public void SetWidth(int w) { Width = w; }
        public void SetHeight(int h) { Height = h; }
    }
}