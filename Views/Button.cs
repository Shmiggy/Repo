namespace SSSG.Views.Buttons
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button
    {
        Texture2D imagePressed, imageUnpressed;
        Rectangle location;
        SpriteBatch spriteBatch;
        MouseState mouse;
        MouseState oldMouse;

        public Button(Texture2D prs, Texture2D unprs, SpriteBatch sBatch)
        {
            imagePressed = prs;
            imageUnpressed = unprs;
            location = new Rectangle(0, 0, imagePressed.Width, imagePressed.Height);
            spriteBatch = sBatch;
        }

        public void Location(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }

        public void Update()
        {
            mouse = Mouse.GetState();

            if ( mouse.LeftButton == ButtonState.Released && oldMouse.LeftButton == ButtonState.Pressed )
            {
                if ( location.Contains(new Point(mouse.X, mouse.Y)) )
                {
                    OnClick(this, new EventArgs());
                }
            }

            oldMouse = mouse;
        }

        public void Draw()
        {
            if ( location.Contains(new Point(mouse.X, mouse.Y)) )
            {
                spriteBatch.Draw(imagePressed, location, Color.White);
            }
            else
            {
                spriteBatch.Draw(imageUnpressed, location, Color.White);
            }
        }

        public event EventHandler OnClick;
    }
}
