using System;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Engine.Utils;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.UI {
    class TextInput : UIEntity {
        const int DEFAULT_MAX_LENGTH = 24;
        const int TIME_BETWEEN_DELETE = 25;
        const int FOCUS_INDEX = 1;
        const int REST_INDEX = 0;
        public string Text { get { return GetComponent<TextComponent>().Text; } }
        int MaxLength;
        bool CanDelete;
        double TimeSinceLastDelete;

        public TextInput(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            MaxLength = DEFAULT_MAX_LENGTH;
            CanDelete = true;
            AddComponent(new FocusComponent(FocusCallback));
            var sprite = new SpriteComponent(game, Sprites.GetSprite("s_medium_container"));
            AddComponent(sprite);
            var text = new TextComponent(game);
            text.Move(16, 0);
            text.SetSize(3);
            AddComponent(text);
            SetDim(sprite.Width, sprite.Height);
        }

        public void SetMaxLength(int newLength) { MaxLength = newLength; }

        void FocusCallback(object sender, EventArgs args) {
            if (GetComponent<FocusComponent>().IsFocused) {
                GetComponent<SpriteComponent>().SetIndex(FOCUS_INDEX);
            } else {
                GetComponent<SpriteComponent>().SetIndex(REST_INDEX);
            }
        }

        Keys GetCharFromInputs(Keys key) {
            if ((int)key >= 48 && (int)key <= 105) {
                return key;
            }

            return Keys.None;
        }

        void AddChar(char character) {
            var textComp = GetComponent<TextComponent>();
            if (textComp.Text.Length < DEFAULT_MAX_LENGTH) {
                var newText = textComp.Text + character;
                textComp.SetText(newText);
                textComp.CenterVertically();
            }
        }

        void RemoveChar() {
            var textComp = GetComponent<TextComponent>();
            if (textComp.Text.Length > 0) {
                var newText = textComp.Text.Substring(0, textComp.Text.Length - 1);
                textComp.SetText(newText);
                textComp.CenterVertically();
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (GetComponent<FocusComponent>().IsFocused) {

                // CHECK FOR DELETE
                if (CanDelete) {
                    if (InputHelper.KeyDown(Keys.Back) || InputHelper.KeyDown(Keys.Delete)) {
                        RemoveChar();
                        CanDelete = false;
                    }
                } else {
                    TimeSinceLastDelete += gameTime.ElapsedGameTime.TotalMilliseconds;
                    if (TimeSinceLastDelete >= TIME_BETWEEN_DELETE) {
                        CanDelete = true;
                        TimeSinceLastDelete = 0;
                    }
                }

                // CHECK FOR SPACES
                if (InputHelper.KeyPressed(Keys.Space)) AddChar(' ');


                Keys[] keys = InputHelper.GetState().GetPressedKeys();

                // CHECK FOR REGULAR
                if (keys.Length > 0) {
                    foreach (var key in keys) {
                        var keyChar = GetCharFromInputs(key);
                        if (keyChar != Keys.None && InputHelper.KeyPressed(keyChar)) AddChar((char)keyChar);
                    }
                }

            }
        }
    }
}