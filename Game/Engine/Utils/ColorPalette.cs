using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;

namespace Engine.Utils {
    class ColorPalette {
        static Color DefaultColor { get { return new Color(255, 255, 255); } }
        static Dictionary<string, Color> Colors { get; set; }
        const string FILE_LOCATION = "Content/Configs/Colors.xml";

        // Read from the color file and set the properties
        static ColorPalette() {
            Colors = new Dictionary<string, Color>();

            XmlDocument doc = new XmlDocument();
            doc.Load(FILE_LOCATION);

            foreach (XmlNode cellData in doc.DocumentElement.ChildNodes) {
                var name = cellData.Attributes["name"].InnerText;
                var r = int.Parse(cellData.Attributes["r"].InnerText);
                var g = int.Parse(cellData.Attributes["g"].InnerText);
                var b = int.Parse(cellData.Attributes["b"].InnerText);
                Colors.Add(name, new Color(r,g,b));
            }
        }

        public static Color GetColor(string key) {
            if (Colors.ContainsKey(key)) {
                return Colors[key];
            }

            return DefaultColor;
        }
        
    }
}