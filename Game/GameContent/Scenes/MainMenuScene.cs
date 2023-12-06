using Engine.Core;
using Engine.Utils;
using GameContent.UI;
using Microsoft.Xna.Framework;

namespace GameContent.Scenes {
    class MainMenuScene : Scene {
        
        public MainMenuScene(Game game) : base() {
            Initialize(game);
            BackgroundColor = ColorPalette.GetColor("background");
            AddEntity(new MainMenuUIManager(game, _events));
            //game.Services.GetService<DisplayManager>().ToggleFullScreen();
        }

        public override void Update(GameTime gameTime) {
            UpdateFPS(gameTime);
        }

        void UpdateFPS(GameTime gameTime) {
            var time = gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
            var time2 = gameTime.ElapsedGameTime.TotalMilliseconds;
            var fps = 1000f / (time2 - time);
            _game.Window.Title = fps.ToString() + " FPS";
        }
    }
}