using System;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class Button : UIEntity {
        const float TEXT_SIZE = 4.5f;
        const int FOCUS_INDEX = 1;
        const int REST_INDEX = 0;
        public Button(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            var spriteData = Sprites.GetSprite("s_small_container");
            AddComponent(new SpriteComponent(game, spriteData));
            InitText(game);
            AddComponent(new FocusComponent(FocusHandler));
            AddComponent(new ActionComponent());
            var sprite = GetComponent<SpriteComponent>();
            SetDim(sprite.Width, sprite.Height);
        }

        void InitText(Game game) {
            var text = new TextComponent(game);
            text.SetSize(TEXT_SIZE);
            text.SetColor(ColorPalette.GetColor("light_gray"));
            AddComponent(text);
        }

        public void FocusHandler(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
                GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("white"));
            } else {
                GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
                GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("light_gray"));
            }
        }

        public void SetText(string text) {
            var textComp = GetComponent<TextComponent>();
            textComp.SetText(text);
            textComp.Center();
        }

        public void SetAction(EventHandler handler) {
            GetComponent<ActionComponent>().SetAction(handler);
        }

        public override void SetScale(Vector2 scale) {
            base.SetScale(scale);
            var textComp = GetComponent<TextComponent>();
            textComp.Center();
        }
    }
}