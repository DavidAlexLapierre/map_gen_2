using System;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class NameField : UIEntity {
        const int X_OFFSET = 52;
        const int FOCUS_INDEX = 1;
        const int REST_INDEX = 0;
        Text Name;
        TextInput Input;
        public NameField(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            Name = new Text(game, events, manager);
            Name.GetComponent<TextComponent>().SetText("World name:");
            Name.GetComponent<TextComponent>().Move(0, 12);
            AddChild(Name);
            Input = new TextInput(game, events, manager);
            AddChild(Input);
            AddComponent(new FocusComponent(FocusCallback));
        }

        void FocusCallback(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                Input.GetComponent<FocusComponent>().ToggleFocus();
                Input.GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
                Name.GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("white"));
            } else {
                Input.GetComponent<FocusComponent>().ToggleFocus();
                Input.GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
                Name.GetComponent<TextComponent>().SetColor(ColorPalette.GetColor("light_gray"));
            }
        }

        public override void SetPos(float x, float y) {
            base.SetPos(x, y);
            var pos = GetComponent<PositionComponent>().Coords;
            Name.SetPos(pos.X, pos.Y);
            Input.SetPos(pos.X + X_OFFSET, pos.Y);
        }

        public string GetValue() {
            return Input.Text;
        }
    }
}