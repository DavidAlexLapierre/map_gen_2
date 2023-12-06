using Engine.Components;
using Engine.Events;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class VersionInfo : UIEntity{
        public VersionInfo(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            var text = new TextComponent(game);
            text.SetSize(4);
            text.SetText("Version 0.0.1");
            AddComponent(text);
            GetComponent<DimensionComponent>().SetWidth((int)text.Width);
            GetComponent<DimensionComponent>().SetHeight((int)text.Height);
        }
    }
}