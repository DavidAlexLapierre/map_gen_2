using Engine.Components;
using Engine.Core;
using Engine.Events;
using Microsoft.Xna.Framework;

namespace Engine.UI {
    class UIEntity : Entity {

        protected UIManager Manager;
        public UIEntity(Game game, SceneEvents events, UIManager manager) : base(game, events) {
            Manager = manager;
            AddComponent(new ScaleComponent());
            AddComponent(new PositionComponent());
            AddComponent(new DimensionComponent());
        }

        public virtual void SetScale(Vector2 scale) {
            GetComponent<ScaleComponent>().SetScale(scale);
            foreach (var child in Children.Values) {
                (child as UIEntity).SetScale(scale);
            }
        }

        public virtual void SetPos(float x, float y) {
            GetComponent<PositionComponent>().SetCoords(x, y);
        }

        public void SetTextDepth(float depth) {
            var text = GetComponent<TextComponent>();
            if (text != null) text.SetDepth(depth);
        }

        public void SetSpriteDepth(float depth) {
            var sprite = GetComponent<SpriteComponent>();
            if (sprite != null) sprite.SetDepth(depth);
        }

        public Point GetDim() {
            var dim = GetComponent<DimensionComponent>();
            return new Point(dim.Width, dim.Height);
        }

        public virtual void SetDim(int w, int h) {
            var dim = GetComponent<DimensionComponent>();
            dim.SetWidth(w);
            dim.SetHeight(h);
        }
    }
}