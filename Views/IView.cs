namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SSSG.Models;

    public interface IView
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        void Initialize(SpriteBatch spriteBatch);

        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="model">the current game model</param>
        void Update(GameTime gameTime, IGameModel model);

        /// <summary>
        /// Draws the view.
        /// </summary>
        /// <param name="model">the current game model</param>
        void Draw(IGameModel model);
    }
}
