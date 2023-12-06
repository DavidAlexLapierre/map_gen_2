using Microsoft.Xna.Framework;

namespace Engine.Utils {

    class Resolutions {
        public static Point _16_9_320x_180y { get { return new Point(320, 180); } }
        public static Point _4_3_640x_480y { get { return new Point(640, 480); } }
        public static Point _16_9_640x_360y { get { return new Point(640, 360); } }
        public static Point _4_3_1280x_960y { get { return new Point(1280, 960); } }
        public static Point _16_9_1280x_720y { get { return new Point(1280, 720); } }
        public static Point _16_9_1920x_1080y { get { return new Point(1920, 1080); } }
    }
}