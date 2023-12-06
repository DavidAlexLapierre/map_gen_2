using System;
using Engine.Core;
using Engine.Utils;
using GameContent.Controllers;
using GameContent.Generation;
using GameContent.Models;
using GameContent.UI;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.Scenes {
    class WorldMapScene : Scene {
        WMSelectionController _controller;

        public WorldMapScene(Game game) : base() {
            Initialize(game);
            BackgroundColor = ColorPalette.GetColor("background");
        }
        
        public override void Init(string initData = ""){
            Console.WriteLine("INITIALIZING MAP EXPLORER SCENE");
            WorldData data = WorldDataController.GetDataFromName(initData);
            Generator.Init(data.Seed);
            _controller = new WMSelectionController(_game, _events);
            AddEntity(_controller);
        }

        public override void Update(GameTime gameTime) {
            var time = gameTime.ElapsedGameTime.TotalMilliseconds;
            base.Update(gameTime);
            ActualUpdate(gameTime);
            var time2 = gameTime.ElapsedGameTime.TotalMilliseconds;
            var fps = 1000f / (time2 - time);
            _game.Window.Title = fps.ToString() + " FPS";
        }

        void ActualUpdate(GameTime gameTime) {
            if (InputHelper.KeyPressed(Keys.Enter)) {
                _controller.Clear();
                _controller.InitTiles();
            }
        }
    }
}