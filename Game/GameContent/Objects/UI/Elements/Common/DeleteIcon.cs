using System;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class DeleteIcon : UIEntity {
        const float TEXT_SIZE = 4.5f;
        const int FOCUS_INDEX = 1;
        const int REST_INDEX = 0;
        public DeleteIcon(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            var spriteData = Sprites.GetSprite("s_delete_icon");
            AddComponent(new SpriteComponent(game, spriteData));
            AddComponent(new FocusComponent(FocusHandler));
            AddComponent(new ActionComponent());
            var sprite = GetComponent<SpriteComponent>();
            SetDim(sprite.Width, sprite.Height);
        }

        public void FocusHandler(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
            } else {
                GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
            }
        }

        public void SetAction(EventHandler handler) {
            GetComponent<ActionComponent>().SetAction(handler);
        }
    }
}