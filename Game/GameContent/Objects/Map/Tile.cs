using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.Models;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.Objects {
    class Tile : Entity {
        public Tile(Game game, SceneEvents events, SpriteData data) : base(game, events) {
            var sprite = new SpriteComponent(game, data);
            sprite.SetIndex(Generator.Next(data.Sprites.Count));
            AddComponent(sprite);
            AddComponent(new PositionComponent());
        }

        public void SetPosition(int x, int y) {
            GetComponent<PositionComponent>().SetCoords(x, y);
        }

        public void SetDepth(float depth) { GetComponent<SpriteComponent>().SetDepth(depth); }
    }
}