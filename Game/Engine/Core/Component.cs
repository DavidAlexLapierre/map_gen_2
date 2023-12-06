using Microsoft.Xna.Framework;

namespace Engine.Core {
    abstract class Component {
        public Entity Parent { get; set; }
        public Component() {}
        public virtual void Update(GameTime gameTime) {}
        public virtual void Draw(GameTime gameTime) {}
    }
}
