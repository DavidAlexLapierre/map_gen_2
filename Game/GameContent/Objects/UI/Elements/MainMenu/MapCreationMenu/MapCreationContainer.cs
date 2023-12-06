using Engine.Components;
using Engine.Events;
using Engine.UI;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class MapCreationContainer : UIEntity {
        public Point NameFieldPos { get; private set; }
        public Point CreateMapBtnPos { get; private set; }
        public Point BackBtnPos { get; private set; }
        public MapCreationContainer(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            NameFieldPos = new Point(7, 16);
            CreateMapBtnPos = new Point(5, 56);
            BackBtnPos = new Point(75, 56);
            InitSprite(game);
        }

        void InitSprite(Game game) {
            var data = Sprites.GetSprite("s_map_creation_menu");
            var sprite = new SpriteComponent(game, data);
            SetDim(sprite.Width, sprite.Height);
            AddComponent(sprite);
        }
    }
}