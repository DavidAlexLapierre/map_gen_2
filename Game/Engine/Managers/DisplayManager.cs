using Engine.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Managers {
    class DisplayManager {
        public int ViewWidth { get; private set; }
        public int ViewHeight { get; private set; }
        public int RenderWidth { get; private set; }
        public int RenderHeight { get; private set; }
        public float ScaleX { get; private set; }
        public float ScaleY { get; private set; }

        GraphicsDeviceManager _graphics { get; set; }
        GraphicsDevice _graphicsDevice { get; set; }
        public Matrix Scale {get; private set;}


        public DisplayManager(Game game) {
            _graphics = (GraphicsDeviceManager)game.Services.GetService(typeof(GraphicsDeviceManager));
            _graphicsDevice = game.GraphicsDevice;
            game.IsMouseVisible = true;
            SetResolution(Resolutions._16_9_1280x_720y, Resolutions._16_9_640x_360y);
        }

        public void ToggleFullScreen() {
            _graphics.IsFullScreen = !_graphics.IsFullScreen;
            if (_graphics.IsFullScreen) {
                var w = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                var h = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
                _graphics.HardwareModeSwitch = false;
                SetResolution(new Point(w, h), new Point(ViewWidth, ViewHeight));
            } else {
                _graphics.HardwareModeSwitch = true;
                SetResolution(new Point(RenderWidth, RenderHeight), new Point(ViewWidth, ViewHeight));
            }
        }

        public Vector2 ScalePosition(Vector2 position) {
            return new Vector2(position.X / ScaleX, position.Y / ScaleY);
        }

        void GetCurrentDimensions() {
            RenderWidth = _graphics.PreferredBackBufferWidth;
            RenderHeight = _graphics.PreferredBackBufferHeight;
        }

        public void SetResolution(Point render, Point view) {
            RenderWidth = render.X;
            RenderHeight = render.Y;
            ViewWidth = view.X;
            ViewHeight = view.Y;
            _graphics.PreferredBackBufferWidth = RenderWidth;
            _graphics.PreferredBackBufferHeight = RenderHeight;
            _graphics.ApplyChanges();
            SetScaling();
        }

        /// This method will set the scaling of the UI based on the resolution
        void SetScaling() {
            ScaleX = RenderWidth / (float) ViewWidth;
            ScaleY = RenderHeight / (float) ViewHeight;
            var scaleVector = new Vector3(ScaleX, ScaleY, 1);
            Scale = Matrix.CreateScale(scaleVector);
        }
    }
}
