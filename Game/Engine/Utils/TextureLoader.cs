using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Engine.Utils {
    class TextureLoader {
        public static Dictionary<string, Texture2D> Tilesets { get; private set; }

        static TextureLoader() {Tilesets = new Dictionary<string, Texture2D>(); }

        public static Texture2D LoadTexture(GraphicsDevice _graphicsDevice, string path) {
            
            if (!Tilesets.ContainsKey(path)) {
                Texture2D texture;
                using (var stream = new FileStream(path, FileMode.Open)) {
                    texture = Texture2D.FromStream(_graphicsDevice, stream);
                }
                Tilesets.Add(path, texture);
            }

            return Tilesets[path];
        }
    }
}
