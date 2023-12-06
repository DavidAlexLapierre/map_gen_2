using Engine.Core;
using Engine.Utils;
using GameContent.Controllers;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.Scenes {
    class GameScene : Scene {
        
        public GameScene(Game game) : base() {
            Initialize(game);
            BackgroundColor = ColorPalette.GetColor("background");
        }

        public override void Init(string config_path = "") {
            AddEntity(new GameController(_game, _events));
            // TODO: Load the config file

            // TODO: Send the config to server
                // If first init, then generate the first chunks
                // else load data from the save file

            // TODO: If the player is not registered, go to character creation screen
            // TODO: Place the player at the map spawn
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