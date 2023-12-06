using Engine.Components;
using Engine.Models;

namespace GameContent.Models {
    struct TileData {
        public int Value;
        public SpriteData Sprite;
        public TileData(int value, SpriteData sprite) {
            Value = value;
            Sprite = sprite;
        }
    }
}