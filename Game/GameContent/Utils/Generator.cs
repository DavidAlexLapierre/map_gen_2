using System;

namespace GameContent.Utils {
    class Generator {
        static Random Rand = null!;

        public static void Init(int seed) {
            Rand = new Random(seed);
        }

        public static int GetSeed() {
            var rand = new Random();
            return rand.Next(int.MaxValue);
        }

        public static double NextDouble() {
            return Rand.NextDouble();
        }

        public static int Next() {
            return Rand.Next();
        }

        public static int Next(int max) {
            return Rand.Next(max+1);
        }

        public static int Next(int min, int max) {
            return Rand.Next(min, max+1);
        }
    }
}