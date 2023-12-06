using System;
using GameContent.Testing;
using GameContent.Utils;
using Microsoft.Xna.Framework;

namespace GameContent.Generation {
    class WorldGenerator {
        public static float[,] GenerateTerrain() {
            var simplex = new Algo(NoiseType.Simplex, 0.04f, Vector2.Zero, 0, Generator.Next(100));
            var square = new Algo(NoiseType.Square, 1, Vector2.Zero, 0, 2, true);
            var focusMiddle = new Algo(NoiseType.Square, 1, Vector2.Zero, 0, 4, false, true);
            
            var p = new Pass();
            p.AddAlgo(simplex);
            p.AddAlgo(square);
            p.AddAlgo(focusMiddle);

            return p.Apply(Macros.WORLD_W, Macros.WORLD_H);;
        }

        public static float[,] GenerateMountains() {
            int PASS = 5;

            var p = new Pass();

            for (int w = 0; w < PASS; w++) {
                var xx = Generator.Next(100000);
                var yy = Generator.Next(100000);
                var simplex = new Algo(NoiseType.Simplex, 0.09f, new Vector2(xx, yy), 0, Generator.Next(100));
                p.AddAlgo(simplex);
            }

            var map = p.Apply(Macros.WORLD_W, Macros.WORLD_H);

            // regulate values
            float min =0;
            float max = 0;

            for (int i = 0; i < Macros.WORLD_W; i++) {
                for (int j = 0; j < Macros.WORLD_H; j++) {
                    if (map[i,j] < min) min = map[i,j];
                    if (map[i,j] > max) max = map[i,j];
                }
            }

            var divider = Math.Max(Math.Abs(min), max);

            for (int i = 0; i < Macros.WORLD_W; i++) {
                for (int j = 0; j < Macros.WORLD_H; j++) {
                    map[i,j] = map[i,j] / divider;
                }
            }


            return map;
        }
    }
}