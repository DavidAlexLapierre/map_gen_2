using System.Text;
using Engine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine.Components {
    class TextComponent : Component {
        const float BASE_SIZE = 12.0f;
        public float Size { get; private set; }
        public float Width { get { 
            var scale = Parent.GetComponent<ScaleComponent>().Scale;
            return Font.MeasureString(Text).X * Size / scale.X; } }
        public float Height { get {
            var scale = Parent.GetComponent<ScaleComponent>().Scale;
            return Font.MeasureString(Text).Y * Size / scale.Y; } }
        public string Text { get; private set; }
        public Color Color { get; private set; }
        public Vector2 Position { get; private set; }
        public float Depth { get; private set; }
        Vector2 RenderPosition;
        SpriteBatch _spriteBatch;
        SpriteFont Font;

        public TextComponent(Game game) : base() {
            _spriteBatch = (SpriteBatch)game.Services.GetService<SpriteBatch>();
            Font = game.Content.Load<SpriteFont>("Fonts/MainFont");
            Color = Color.White;
            Position = new Vector2();
            Size = 5 / 12.0f;
            Depth = 1f;
            Text = "";
        }

        public void SetSize(float size) { Size = size / BASE_SIZE; }
        public void SetColor(Color color) { Color = color; }
        public void SetDepth(float depth) { Depth = depth; }

        public void SetText(string text) {
            Text = text;
        }
        
        public override void Update(GameTime gameTime){
            var parentPos = Parent.GetComponent<PositionComponent>();
            var scale = Parent.GetComponent<ScaleComponent>().Scale;
            RenderPosition = Position + parentPos.Coords;
        }

        public override void Draw(GameTime gameTime) {
            _spriteBatch.DrawString(Font, Text, RenderPosition, Color, 0, Vector2.Zero, Size, SpriteEffects.None, Depth);
        }

        public void Center() {
            if (Parent != null) {
                CenterHorizontally();
                CenterVertically();
            }
        }

        public void CenterHorizontally() {
            var dim = Parent.GetComponent<DimensionComponent>();
            var scaleComponent = Parent.GetComponent<ScaleComponent>();
            if (dim != null && scaleComponent != null) {
                var scale = scaleComponent.Scale;
                var w = Font.MeasureString(Text).X * Size / scale.X;
                var containerW = dim.Width;
                var xx = (containerW - w) / 2.0f * scale.X;
                Position = new Vector2(xx, Position.Y);
            }
        }

        public void CenterVertically() {
            var dim = Parent.GetComponent<DimensionComponent>();
            var scaleComponent = Parent.GetComponent<ScaleComponent>();
            if (dim != null && scaleComponent != null) {
                var scale = scaleComponent.Scale;
                var h = Font.MeasureString(Text).Y * Size / scale.Y;
                var containerH = dim.Height;
                var yy = (containerH - h) / 2.0f * scale.Y;
                Position = new Vector2(Position.X, yy);
            }
        }

        public void Move(int x, int y) {
            Position += new Vector2(x, y);
        }

        string WrapText(SpriteFont spriteFont, string text, float maxLineWidth) {
            string[] words = text.Split(' ');
            StringBuilder sb = new StringBuilder();
            float lineWidth = 0f;
            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
        
    }
}