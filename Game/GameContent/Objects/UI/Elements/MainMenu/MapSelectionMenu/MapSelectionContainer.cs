using Engine.Components;
using Engine.Events;
using Engine.UI;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class MapSelectionContainer : UIEntity {
        public Point InitialWorldListPos { get; private set; }
        public Point NoMapMessagePos { get; private set; }
        public Point NewMapBtnPos { get; private set; }
        public Point BackBtnPos { get; private set; }
        public MapSelectionContainer(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            InitialWorldListPos = new Point(16, 10);
            NoMapMessagePos = new Point(24, 32);
            NewMapBtnPos = new Point(5, 120);
            BackBtnPos = new Point(75, 120);
            InitSprite(game);
        }

        void InitSprite(Game game) {
            var data = Sprites.GetSprite("s_map_selection_menu");
            var sprite = new SpriteComponent(game, data);
            SetDim(sprite.Width, sprite.Height);
            AddComponent(sprite);
        }
    }
}