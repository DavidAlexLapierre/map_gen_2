using System.Collections.Generic;
using GameContent.Utils;
using Engine.Components;
using Engine.Events;
using Engine.UI;
using Microsoft.Xna.Framework;

namespace GameContent.UI {
    class ActionBar : UIEntity {
        const int NB_ITEM = 6;
        public List<ActionBarItem> Items { get; private set; }
        public ActionBar(Game game, SceneEvents events, UIManager manager) : base(game, events, manager) {
            Items = new List<ActionBarItem>();
            InitBar(game, events);
        }

        void InitBar(Game game, SceneEvents events) {
            var pos = GetComponent<PositionComponent>().Coords;
            for (int i = 0; i < NB_ITEM; i++) {
                var item = new ActionBarItem(game, events, Manager);
                item.SetPos(pos.X + i * Macros.CELL_DIM, pos.Y);
                Items.Add(item);
                AddChild(item);
            }
        }

        void SetItemPosition() {
            var scale = GetComponent<ScaleComponent>().Scale;
            var pos = GetComponent<PositionComponent>().Coords;
            int counter = 0;
            foreach (var item in Items) {
                item.GetComponent<PositionComponent>().SetCoords(pos.X + counter * Macros.CELL_DIM * scale.X, pos.Y);
                ++counter;
            }
        }

        public override void SetScale(Vector2 scale) {
            base.SetScale(scale);
            SetItemPosition();
        }

        public override void SetPos(float x, float y) {
            base.SetPos(x, y);
            SetItemPosition();
        }
    }
}