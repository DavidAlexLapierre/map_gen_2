using Engine.Core;
using Engine.Events;
using Engine.Managers;
using Engine.Utils;
using Microsoft.Xna.Framework;

namespace Engine.UI {
    class UIManager : Entity {
        public int UI_W { get; protected set; }
        public int UI_H { get; protected set; }
        public Vector2 Scale { get; protected set; }
        DisplayManager _displayManager;

        public UIManager(Game game, SceneEvents events) : base(game, events) {
            _displayManager = (DisplayManager)game.Services.GetService<DisplayManager>();
            SetResolution(Resolutions._16_9_320x_180y);
        }

        public void SetResolution(Point res) {
            UI_W = res.X;
            UI_H = res.Y;
            SetScaling();
        }

        protected void SetScaling() {
            var scaleX = _displayManager.ViewWidth / (float) UI_W;
            var scaleY = _displayManager.ViewHeight / (float) UI_H;
            Scale = new Vector2(scaleX, scaleY);
        }
    }
}