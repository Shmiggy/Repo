namespace SSSG.Views.Buttons
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public class Button
    {
        private Texture2D imagePressed;     // texture for hover
        private Texture2D imageUnpressed;   // texture for not hover
        private Rectangle location;         // location on the screen
        private SpriteBatch spriteBatch;    // the sprite batch
        private MouseState mouse;           // mouse info provider (current state)
        private MouseState oldMouse;        // old mouse state

        /// <summary>
        /// Initializes a new instance of Button class.
        /// </summary>
        /// <param name="prs">texture for hover</param>
        /// <param name="unprs">texture for not hover</param>
        /// <param name="sBatch">the sprite batch</param>
        public Button(Texture2D prs, Texture2D unprs, SpriteBatch sBatch)
        {
            imagePressed = prs;
            imageUnpressed = unprs;
            location = new Rectangle(0, 0, imagePressed.Width, imagePressed.Height);
            spriteBatch = sBatch;
        }

        /// <summary>
        /// Sets the location for the button.
        /// </summary>
        /// <param name="x">x coordiante</param>
        /// <param name="y">y coordiante</param>
        public void Location(int x, int y)
        {
            location.X = x;
            location.Y = y;
        }

        /// <summary>
        /// Update the button
        /// </summary>
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

        /// <summary>
        /// Draws the button.
        /// </summary>
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

        /// <summary>
        /// Fires when the button is clicked.
        /// </summary>
        public event EventHandler OnClick;
    }
}
