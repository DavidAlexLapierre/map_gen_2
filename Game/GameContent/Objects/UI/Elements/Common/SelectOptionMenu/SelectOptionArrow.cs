using Engine.Components;
using Engine.Events;
using Engine.UI;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class SelectOptionArrow : UIEntity {
        public const int FOCUS_INDEX = 1;
        public const int REST_INDEX = 0;

        public SelectOptionArrow(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            var spriteData = Sprites.GetSprite("s_option_arrow");
            AddComponent(new SpriteComponent(game, spriteData));
            AddComponent(new ActionComponent());
            var sprite = GetComponent<SpriteComponent>();
            SetDim(sprite.Width, sprite.Height);
        }

        public void Flip() {
            GetComponent<SpriteComponent>().SetFlip(true);
        }

        public void Focus() {
            GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
        }

        public void RemoveFocus() {
            GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
        }
    }
}