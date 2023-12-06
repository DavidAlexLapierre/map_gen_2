using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Core;
using Engine.Utils;
using System;
using System.Collections.Generic;

namespace Engine.Managers {
    class SceneManager : DrawableGameComponent {
        private GraphicsDevice _graphicsDevice { get; set; }
        private Dictionary<Guid, Scene> Scenes { get; set; }
        private Scene CurrentScene { get; set; }
        Stack<Scene> SceneStack { get; set; }

        public SceneManager(Game game) : base(game) {}

        /// <summary>
        /// Initialize the SceneManager once the Game is finished initializing
        /// </summary>
        /// <param name="game">The Game object</param>
        public void Init(Game game, List<Scene> scenes, Scene startScene) {
            Scenes = new Dictionary<Guid, Scene>();
            _graphicsDevice = game.GraphicsDevice;
            SceneStack = new Stack<Scene>();
            InitializeScenes(game, scenes, startScene);
        }

        public void ChangeScene<T>(string initData = "") {
            try {
                foreach (var id in Scenes.Keys) {
                    if (Scenes[id].GetType() == typeof(T)) {
                        SceneStack.Push(CurrentScene);
                        CurrentScene = Scenes[id];
                        CurrentScene.Init(initData);
                        break;
                    }
                }
            } catch(Exception e) {
                Console.WriteLine(e.Message);
                Console.WriteLine("ERROR: Cannot switch scene");
            }
        }

        public void GoToPreviousScene() {
            try {
                CurrentScene = SceneStack.Pop();
            } catch(Exception) {
                Console.WriteLine("Error");
            }
            
        }

        private void InitializeScenes(Game game, List<Scene> _scenes, Scene startScene) {
            CurrentScene = startScene;
            foreach (Scene scene in _scenes) {
                Scenes.Add(scene.Id, scene);
            }
        }

        public override void Draw(GameTime gameTime) {
            _graphicsDevice.Clear(CurrentScene.BackgroundColor);
            CurrentScene.Draw(gameTime);
        }

        /// <summary>
        /// Entry point for the game to display the different scenes
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime) {
            InputHelper.Update(gameTime);
            CurrentScene.Update(gameTime);
        }
    }
}
