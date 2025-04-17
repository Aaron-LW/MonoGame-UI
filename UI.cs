using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Net.Mime;
using MonoGame;
using System;
using System.Globalization;

namespace UI
{
    public enum Align
    {
        None,
        Center,
        TopCenter,
        BottomCenter,
    }

    public class UIElement
    {
        public Texture2D _pixel;

        public Vector2 LocalPosition;
        public Vector2 GlobalPosition;
        public int Width;
        public int Height;
        public UIElement Parent;
        public Align Align;
        public bool Visible;

        public UIElement(GraphicsDevice graphicsDevice, int x, int y, int width, int height, Align align = Align.None, UIElement parent = null, bool visible = true)
        {
            _pixel = new Texture2D(graphicsDevice, 1, 1);
            _pixel.SetData(new[] { Color.White });

            Width = width;
            Height = height;

            LocalPosition = new Vector2(x, y);
            if (Parent != null) { GlobalPosition = Parent.LocalPosition; }
            else { GlobalPosition = LocalPosition; }

            Parent = parent;
            Align = align;
            Visible = visible;
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
            GlobalPosition = UpdatePosition(this);
            spriteBatch.Draw(_pixel, new Rectangle((int)this.GlobalPosition.X, (int)GlobalPosition.Y, (int)Width, (int)Height), Color.Wheat);
        }

        public Vector2 UpdatePosition(UIElement element)
        {
            if (element.Parent != null)
            {
                if (element.Align != Align.None)
                {
                    switch (element.Align)
                    {
                        //Center
                        case Align.Center: { return new Vector2(element.Parent.GlobalPosition.X + ((element.Parent.Width - element.Width) / 2) - element.LocalPosition.X, element.Parent.GlobalPosition.Y + ((element.Parent.Height - element.Height) / 2) - element.LocalPosition.Y); }
                        case Align.TopCenter: { return new Vector2(element.Parent.GlobalPosition.X + (element.Parent.Width - element.Width) / 2, element.Parent.LocalPosition.Y - element.Parent.Height / 2); }
                        case Align.BottomCenter: { return new Vector2(element.Parent.GlobalPosition.X + (element.Parent.Width - element.Width) / 2, element.Parent.LocalPosition.Y + element.Parent.Height / 2 - element.Height); }

                        //Left


                        //Right
                    }
                }

                return new Vector2(element.Parent.GlobalPosition.X - element.LocalPosition.X, element.Parent.GlobalPosition.Y - element.LocalPosition.Y);
            }
            else
            {
                if (element.Align != Align.None)
                {
                    switch (element.Align)
                    {
                        case Align.Center: { return new Vector2(element.LocalPosition.X - element.Width / 2, element.LocalPosition.Y - element.Height / 2); }
                    }
                }
            }

            return new Vector2(element.LocalPosition.X, element.LocalPosition.Y);
        }
        
        public void Hide() { Visible = false; }
        public void Show() { Visible = true; }
        public void ToggleVisibility() { Visible = !Visible; }
    }

    public class Panel : UIElement
    {
        public Color Color;

        public Panel(GraphicsDevice graphicsDevice, int x, int y, int width, int height, Color color, Align align = Align.None, UIElement parent = null, bool visible = true) : base(graphicsDevice, x, y, width, height, align, parent, visible)
        {
            Color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GlobalPosition = UpdatePosition(this);
            spriteBatch.Draw(_pixel, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)Width, (int)Height), Color);
        }
    }
    
    public class Text : UIElement
    {
        private SpriteFont Font;
        public int Size;
        public string TextString;
        public Color Color;
        public float Rotation;
        private Vector2 Origin;
        public float Scale;

        public Text(GraphicsDevice graphicsDevice, int x, int y, string text, SpriteFont font, Color color, float scale, float rotation = 0, Vector2 origin = new Vector2(), int width = 0, int height = 0, Align align = Align.None, UIElement parent = null, bool visible = true) : base(graphicsDevice, x, y, width, height, align, parent, visible) 
        {
            Font = font;
            TextString = text;
            Color = color;
            Rotation = rotation;
            Origin = origin;
            Scale = scale;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GlobalPosition = UpdatePosition(this);
            spriteBatch.DrawString(Font, TextString, GlobalPosition, Color, Rotation, Origin, Scale, SpriteEffects.None, 0);
        }
    }

    public static class UIutils
    {
        public static List<UIElement> UIElements = new List<UIElement>();

        public static Panel CreatePanel(GraphicsDevice graphicsDevice, int x, int y, int width, int height, Color color, Align align = Align.None, UIElement parent = null, bool visible = true)
        {
            UIElements.Add(new Panel(graphicsDevice, x, y, width, height, color, align, parent, visible));
            return UIElements[UIElements.Count - 1] as Panel;
        }

        public static Text CreateText(GraphicsDevice graphicsDevice, int x, int y, string text, SpriteFont font, Color color, float scale, Align align = Align.None, UIElement parent = null, bool visible = true, float rotation = 0, Vector2 origin = new Vector2()) 
        {
            UIElements.Add(new Text(graphicsDevice, x, y, text, font, color, scale, rotation, origin, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y, align, parent, visible));
            return UIElements[UIElements.Count - 1] as Text;
        }
        
        public static void DrawUIElements(SpriteBatch _spriteBatch) 
        {
            foreach (UIElement UIElement in UIElements) 
            {
                if (UIElement.Visible) 
                {
                    UIElement.Draw(_spriteBatch);
                }
            }
        }
    }
}