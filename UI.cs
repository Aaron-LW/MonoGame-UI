using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

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

        public UIElement(GraphicsDevice graphicsDevice, int x, int y, int width, int height, Align align = Align.None, UIElement parent = null)
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
                        case Align.Center: { return new Vector2(element.Parent.GlobalPosition.X + ((element.Parent.Width - element.Width) / 2) - element.LocalPosition.X, element.Parent.GlobalPosition.Y + ((element.Parent.Height - element.Height) / 2)- element.LocalPosition.Y); }
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
    }
    
    public class Panel : UIElement
    {
        public Color Color;

        public Panel(GraphicsDevice graphicsDevice, int x, int y, int width, int height, Color color, Align align = Align.None, UIElement parent = null) : base(graphicsDevice, x, y, width, height, align, parent)
        {
            Color = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            GlobalPosition = UpdatePosition(this);
            spriteBatch.Draw(_pixel, new Rectangle((int)GlobalPosition.X, (int)GlobalPosition.Y, (int)Width, (int)Height), Color);
        }
    }
}