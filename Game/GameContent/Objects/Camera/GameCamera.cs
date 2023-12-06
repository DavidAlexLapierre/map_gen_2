using Engine.Components;
using Engine.Core;
using Engine.Events;
using Engine.Managers;
using Engine.Utils;
using GameContent.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameContent.Objects {
    class GameCamera : Camera {
        const int MOVE_SPEED = 10;
        Entity Target;
        DisplayManager _displayManager;
        public GameCamera(Game game, SceneEvents events) : base(game, events) {
            AddComponent(new PositionComponent());
            game.Services.AddService<Camera>(this);
            _displayManager = game.Services.GetService<DisplayManager>();
        }

        public void SetTarget(Entity target) { Target = target; }

        public override void Update(GameTime gameTime) {
            var move = new Vector2();

            if (InputHelper.KeyDown(Keys.Right)) {
                move = new Vector2(MOVE_SPEED,0);
                if (CanMove((int)move.X, (int)move.Y)) {
                    GetComponent<PositionComponent>().Move(Vector2.Normalize(move) * MOVE_SPEED);
                }
            }
            if (InputHelper.KeyDown(Keys.Left)) {
                move = new Vector2(-MOVE_SPEED,0);
                if (CanMove((int)move.X, (int)move.Y)) {
                    GetComponent<PositionComponent>().Move(Vector2.Normalize(move) * MOVE_SPEED);
                }
            }
            if (InputHelper.KeyDown(Keys.Up)) {
                move = new Vector2(0,-MOVE_SPEED);
                if (CanMove((int)move.X, (int)move.Y)) {
                    GetComponent<PositionComponent>().Move(Vector2.Normalize(move) * MOVE_SPEED);
                }
            }
            if (InputHelper.KeyDown(Keys.Down)) {
                move = new Vector2(0,MOVE_SPEED);
                if (CanMove((int)move.X, (int)move.Y)) {
                    GetComponent<PositionComponent>().Move(Vector2.Normalize(move) * MOVE_SPEED);
                }
            }
        }

        bool CanMove(int x, int y) {
            var pos = GetComponent<PositionComponent>().Coords;
            int w = _displayManager.ViewWidth;
            int h = _displayManager.ViewHeight;

            if (pos.X + x + w > Macros.WORLD_W * Macros.CELL_DIM) return false;
            if (pos.X + x < 0) return false;
            if (pos.Y + y + h > Macros.WORLD_H * Macros.CELL_DIM) return false;
            if (pos.Y + y < 0) return false;

            return true;
        }

        public override bool GetVisibility(PositionComponent entityPos) {
            var pos = GetComponent<PositionComponent>();
            var width = _displayManager.ViewWidth;
            var height = _displayManager.ViewHeight;

            if (entityPos.Coords.X < pos.Coords.X - Macros.CELL_DIM) return false;
            if (entityPos.Coords.Y < pos.Coords.Y - Macros.CELL_DIM) return false;
            if (entityPos.Coords.X >= pos.Coords.X + width + Macros.CELL_DIM) return false;
            if (entityPos.Coords.Y >= pos.Coords.Y + height + Macros.CELL_DIM) return false;

            return true;
        }
    }
}