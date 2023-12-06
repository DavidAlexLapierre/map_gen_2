using Engine.Core;
using Engine.Events;
using Engine.Managers;
using Engine.Utils;
using GameContent.Generation;
using GameContent.Objects;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.Controllers {
    class WMSelectionController : Entity {
        const float WATER_THRESHOLD = 0;
        const float MOUNTAIN_OFFSET = 0.7f;
        int[,] World;

        public WMSelectionController(Game game, SceneEvents events) : base(game, events) {
            World = new int[Macros.WORLD_W, Macros.WORLD_H];
            var _dpManager = (DisplayManager)game.Services.GetService<DisplayManager>();
            _dpManager.SetResolution(Resolutions._16_9_1280x_720y, Resolutions._16_9_1280x_720y);

            var camera = new GameCamera(game, events);
            AddChild(camera);

            InitTiles();
        }

        public void Clear() {
            foreach (var child in Children.Values) {
                child.Discard();
            }
            var camera = new GameCamera(_game, _events);
            AddChild(camera);
        }

        public void InitTiles() {
            InitTerrain();
            InitMountains();
        }

        public void InitTerrain() {
            var map = WorldGenerator.GenerateTerrain();

            for (int i = 0; i < Macros.WORLD_W; i++) {
                for (int j = 0; j < Macros.WORLD_H; j++) {
                    var height = map[i,j];
                    Tile tile;
                    if (height < WATER_THRESHOLD) {
                        tile = new Tile(_game, _events, Sprites.GetSprite("s_map_water"));
                        tile.SetPosition(i * Macros.CELL_DIM, j * Macros.CELL_DIM);
                        World[i,j] = 0;
                    } else {
                        tile = new Tile(_game, _events, Sprites.GetSprite("s_map_land"));
                        tile.SetPosition(i * Macros.CELL_DIM, j * Macros.CELL_DIM);
                        World[i,j] = 1;
                    }
                    tile.SetDepth(1);
                    AddChild(tile);
                }
            }
        }

        public void InitMountains() {
            var map = WorldGenerator.GenerateMountains();

            for (int i = 0; i < Macros.WORLD_W; i++) {
                for (int j = 0; j < Macros.WORLD_H; j++) {
                    var height = map[i,j];
                    Tile tile;
                    if (height > MOUNTAIN_OFFSET && World[i,j] == 1) {
                        tile = new Tile(_game, _events, Sprites.GetSprite("s_map_mountain"));
                        tile.SetPosition(i * Macros.CELL_DIM, j * Macros.CELL_DIM);
                        tile.SetDepth(0);
                        AddChild(tile);
                    }
                }
            }
        }
    }
}