using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameContent.Utils {
    class Pass {
        List<Algo> Algos { get; set; }
        public Pass() {
            Algos = new List<Algo>();
        }

        public float[,] Apply(int w, int h) {
            float[,] valMap = new float[w, h];
            Algos.ForEach((algo) => {
                valMap = GetValues(algo, valMap);
            });

            return valMap;
        }

        float[,] GetValues(Algo algo, float[,] map) {
            var width = map.GetLength(0);
            var height = map.GetLength(1);
            SimplexNoise simplex = new SimplexNoise();
            WorleyNoise worley = new WorleyNoise(width, height, (int) algo.OptionalParam, (int)algo.Roughness);

            for (int i = 0; i < width; i++) {
                for (int j = 0; j < height; j++) {
                    float value = 0;
                    switch(algo.Type) {
                        case NoiseType.Simplex:
                            var center = new Vector3(algo.Center, algo.Depth) * algo.OptionalParam;
                            value = simplex.GetValue(new Vector3(i, j, algo.Depth) * algo.Roughness + center);
                            break;
                        case NoiseType.Worley:
                            value = worley.GetValue(i, j);
                            break;
                        case NoiseType.Square:
                            value = SquareGradient.GetValue(new Point(i, j), width, height, algo.OptionalParam);
                            break;
                    }

                    if (algo.Reverse) value = 1 - value;
                    if (algo.Substract) value = value * -1;

                    map[i,j] += value;

                    if (map[i,j] > 1) map[i,j] = 1;
                    if (map[i,j] < -1) map[i,j] = -1;
                }
            }

            return map;
        }

        public void AddAlgo(Algo algo) { Algos.Add(algo); }
    }
}