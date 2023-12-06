using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.UI;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class ActionBarItem : UIEntity {
        public ActionBarItem(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            InitSprite(game);
        }

        void InitSprite(Game game) {
            var data = Sprites.GetSprite("s_action_bar_icon");
            var sprite = new SpriteComponent(game, data);
            AddComponent<SpriteComponent>(sprite);
        }

        public void SetPosition(Vector2 pos) {
            GetComponent<PositionComponent>().SetCoords(pos.X, pos.Y);
        }
    }
}