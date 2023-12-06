using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Managers;
using Engine.Utils;
using GameContent.Scenes;
using System;

namespace Maezora {
    public class Main : Game {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneManager _sceneManager;
        private SceneList _sceneList;

        public Main() {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _sceneManager = new SceneManager(this);
            Components.Add(_sceneManager);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            RegisterServices();
            InputHelper.SetVisualManager(this);
            _sceneList = new SceneList(this);
            _sceneManager.Init(this, _sceneList.Scenes, _sceneList.StartingScene);
            base.Initialize();
        }

        void RegisterServices() {
            Services.AddService(typeof(GraphicsDeviceManager), _graphics);
            Services.AddService(typeof(SpriteBatch), _spriteBatch);
            Services.AddService(typeof(DisplayManager), new DisplayManager(this));
            Services.AddService(typeof(SceneManager), _sceneManager);
        }   
    }
}
