using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Engine.Managers;

namespace Engine.Utils {
    class InputHelper {
        static DisplayManager _displayManager { get; set; }
        public static KeyboardState CurrentState { get; private set; }
        public static MouseState CurrentMouseState { get; private set; }
        static KeyboardState PreviousState;
        static bool CanLeftClick { get; set; }
        static bool CanRightClick { get; set; }

        public static void SetVisualManager(Game game) {
            _displayManager = game.Services.GetService<DisplayManager>();
        }
        
        static InputHelper() {
            CurrentState = Keyboard.GetState();
            CanLeftClick = true;
            CanRightClick = true;
        }

        public static KeyboardState GetState() {
            CurrentState = Keyboard.GetState();
            if (PreviousState != CurrentState) {
                return CurrentState;
            }

            return new KeyboardState();
        }

        public static bool KeyPressed(Keys key) {
            CurrentState = Keyboard.GetState();
            if (!PreviousState.IsKeyDown(key) && CurrentState.IsKeyDown(key)) {
                return CurrentState.IsKeyDown(key);
            }

            return false;
        }

        public static bool KeyDown(Keys key) {
            CurrentState = Keyboard.GetState();
            return CurrentState.IsKeyDown(key);
        }

        public static bool KeyReleased(Keys key) {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
            return (CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key));
        }

        public static bool LeftButtonPressed() {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.LeftButton == ButtonState.Pressed && CanLeftClick) {
                CanLeftClick = false;
                return true;
            }
            return false;
        }

        public static bool RightButtonPressed() {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.RightButton == ButtonState.Pressed && CanRightClick) {
                CanRightClick = false;
                return true;
            }
            return false;
        }

        public static bool LeftButtonReleased() {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.LeftButton == ButtonState.Released) {
                CanLeftClick = true;
                return true;
            }
            return false;
        }

        public static bool RightButtonReleased() {
            CurrentMouseState = Mouse.GetState();
            if (CurrentMouseState.RightButton == ButtonState.Released) {
                CanRightClick = true;
                return true;
            }
            return false;
        }

        public static Vector2 GetMousePosition() {
            CurrentMouseState = Mouse.GetState();
            var position = new Vector2(CurrentMouseState.X, CurrentMouseState.Y);
            var scaledPosition = _displayManager.ScalePosition(position);
            return scaledPosition;
        }

        public static void Update(GameTime gameTime) {
            PreviousState = CurrentState;
            LeftButtonReleased();
            RightButtonReleased();
        }
    }
}
