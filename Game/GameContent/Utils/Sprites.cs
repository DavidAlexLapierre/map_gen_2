using System.Collections.Generic;
using System.Xml;
using Engine.Models;
using Microsoft.Xna.Framework;

namespace GameContent.Utils {
    class Sprites {
        static Dictionary<string, SpriteData> _sprites { get; set; }
        const string FILE_LOCATION = "Content/Configs/Sprites.xml";

        // Read from the color file and set the properties
        static Sprites() {
            _sprites = new Dictionary<string, SpriteData>();

            XmlDocument doc = new XmlDocument();
            doc.Load(FILE_LOCATION);

            foreach (XmlNode spriteSet in doc.DocumentElement.ChildNodes) {
                var textureSrc = spriteSet.Attributes["src"].InnerText;
                foreach (XmlNode sprite in spriteSet.ChildNodes) {
                    var name = sprite.Attributes["name"].InnerText;
                    var animationSpd = int.Parse(sprite.Attributes["spd"].InnerText);
                    var sprites = new List<Rectangle>();
                    foreach (XmlNode rect in sprite) {
                        var x = int.Parse(rect.Attributes["x"].InnerText);
                        var y = int.Parse(rect.Attributes["y"].InnerText);
                        var w = int.Parse(rect.Attributes["w"].InnerText);
                        var h = int.Parse(rect.Attributes["h"].InnerText);
                        sprites.Add(new Rectangle(x, y, w, h));
                    }
                    var data = new SpriteData(animationSpd, sprites, textureSrc);
                    _sprites.Add(name, data);
                }
            }
        }

        public static SpriteData GetSprite(string name) {
            return _sprites[name];
        }
    }
}