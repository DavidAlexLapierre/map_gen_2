using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Engine.Models {
    struct SpriteData {
        public int AnimationSpd;
        public List<Rectangle> Sprites;
        public string Texture;

        public SpriteData(int animationSpd, List<Rectangle> sprites, string texture) {
            AnimationSpd = animationSpd;
            Sprites = sprites;
            Texture = texture;
        }
    }
}