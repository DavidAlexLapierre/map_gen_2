using Engine.Components;
using Engine.Events;
using Microsoft.Xna.Framework;

namespace Engine.UI {
    class Text : UIEntity {
        public TextComponent Value { get { return GetComponent<TextComponent>(); } }
        public Text(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            AddComponent(new TextComponent(game));
        }

        public void SetText(string text) {
            GetComponent<TextComponent>().SetText(text);
        }

        public override void SetDim(int w = 0, int h = 0) {
            var textComp = GetComponent<TextComponent>();
            base.SetDim((int)textComp.Width, (int)textComp.Height);
        }
    }
}