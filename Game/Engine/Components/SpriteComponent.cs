using Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Engine.Utils;
using System.Collections.Generic;
using Engine.Models;

namespace Engine.Components {

    class SpriteComponent : Component {
        public int ImageIndex { get; private set; }

        public Color Color { get; private set; }
        public Texture2D Texture { get; private set; }
        public float Depth { get; private set; }
        public bool IsVisible { get; private set; }
        public int Width { get { return Sprite[ImageIndex].Width; } }
        public int Height { get { return Sprite[ImageIndex].Height; } }
        SpriteBatch _spriteBatch;
        GraphicsDevice _graphicsDevice;
        SpriteEffects Flip;
        Camera _camera;
        PositionComponent Pos;
        List<Rectangle> Sprite;
        int AnimationSpeed;
        double CurrentTime;

        public SpriteComponent(Game game, SpriteData data) {
            Flip = SpriteEffects.None;
            _spriteBatch = (SpriteBatch)game.Services.GetService<SpriteBatch>();
            _graphicsDevice = game.GraphicsDevice;
            Sprite = data.Sprites;
            Depth = 1.0f;
            Color = ColorPalette.GetColor("white");
            IsVisible = true;
            Texture = TextureLoader.LoadTexture(_graphicsDevice, data.Texture);
            AnimationSpeed = data.AnimationSpd;
            _camera = game.Services.GetService<Camera>();
        }

        public void SetTexture(string path) { Texture = TextureLoader.LoadTexture( _graphicsDevice, path); }
        public Rectangle GetSprite() { return Sprite[ImageIndex]; }
        public void SetSprite(Rectangle sprite) { Sprite[0] = sprite; }
        public void SetSprite(List<Rectangle> sprites) { 
            Sprite = sprites;
        }
        public void SetColor(string colorName) { Color = ColorPalette.GetColor(colorName); }
        public void SetAnimationSpeed(int speed) { AnimationSpeed = speed; }
        public void SetDepth(float depth) { Depth = depth; }
        public void Hide() { IsVisible = false; }
        public void Show() { IsVisible = true; }

        public void SetFlip(bool isFacingLeft) {
            if (isFacingLeft) {
                Flip = SpriteEffects.FlipHorizontally;
            } else { Flip = SpriteEffects.None; }
        }

        public void SetIndex(int index) {
            if (index >= Sprite.Count) {
                ImageIndex = Sprite.Count - 1;
            } else {
                ImageIndex = index;
            }
        }

        public override void Update(GameTime gameTime) {
            if (Pos == null) Pos = Parent.GetComponent<PositionComponent>();
            if (Sprite.Count > 1 && AnimationSpeed > 0) {
                CurrentTime += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (CurrentTime >= AnimationSpeed) { 
                    CurrentTime = 0;
                    ImageIndex = (ImageIndex+1) % Sprite.Count; 
                }
            }
            if (_camera != null) IsVisible = _camera.GetVisibility(Pos);
        }

        public override void Draw(GameTime gameTime) {
            if (IsVisible && Texture != null && Pos != null) {
                var screenPos = new Vector2(Pos.Coords.X, Pos.Coords.Y);
                if (_camera != null) {
                    var cameraPos = _camera.GetComponent<PositionComponent>();
                    screenPos -= new Vector2(cameraPos.Coords.X, cameraPos.Coords.Y);
                }
                var scale = (Parent != null && Parent.GetComponent<ScaleComponent>() != null) ? Parent.GetComponent<ScaleComponent>().Scale : Vector2.One;
                _spriteBatch.Draw(Texture, screenPos, Sprite[ImageIndex], Color, 0, Vector2.Zero, scale, Flip, Depth);
            }
        }
    }
}