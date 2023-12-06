using Microsoft.Xna.Framework;
using Engine.Core;
using System.Collections.Generic;

namespace GameContent.Scenes {
    class SceneList {
        public Scene StartingScene { get; private set; }
        public List<Scene> Scenes { get; private set; }

        public SceneList(Game game) {
            Scenes = new List<Scene>();
            InitScenes(game);
        }

        /// <summary>
        /// Add new game scene here
        /// </summary>
        private void InitScenes(Game game) {
            StartingScene = new MainMenuScene(game);
            Scenes.Add(StartingScene);
            Scenes.Add(new WorldMapScene(game));
            Scenes.Add(new GameScene(game));
            StartingScene.Init();
        }
    }
}
