using Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameContent.Testing {
    class MapConverter {
        const int MAX_VAL = 255;
        const float WATER_THRESHOLD = 0f;
        static Game _game;
        static SpriteBatch _spriteBatch;
        static Rectangle Sprite;
        static Texture2D Texture;

        public static void Convert(Game game, float[,] map) {
            _game = game;
            _spriteBatch = game.Services.GetService<SpriteBatch>();

            var width = map.GetLength(0);
            var height = map.GetLength(1);

            Sprite = new Rectangle(0, 0, width, height);

            Texture = new Texture2D(game.GraphicsDevice, width, height);
            Color[] data = new Color[width * height];
            for (int pixel = 0; pixel < data.Length; pixel++) {
                var i = pixel / width;
                var j = pixel % height;

                //var val = (int)(map[i,j] * MAX_VAL);
                //data[pixel] = new Color(val, val, val);

                
                var val = map[i,j];
                if (val <= WATER_THRESHOLD) {
                    data[pixel] = ColorPalette.GetColor("test_water");
                } else {
                    data[pixel] = ColorPalette.GetColor("test_land");
                }
                
            }
            Texture.SetData(data);
        }

        public static void Draw() {
            _spriteBatch.Begin();
            _spriteBatch.Draw(Texture, new Vector2(400, 100), Sprite, Color.White, 0, Vector2.Zero, 2, SpriteEffects.None, 0);
            _spriteBatch.End();
        }
    }
}