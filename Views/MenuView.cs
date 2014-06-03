namespace SSSG.Views
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using SSSG.Models;
    using SSSG.Utils.Assets;
    using SSSG.Views.Buttons;
    using System;

    public class MenuView : IView
    {
        private SpriteBatch spriteBatch;        // the sprite batch

        private Texture2D menuBackground;       // background texture
        private Texture2D menuPlayButtonHover;  // hover texture for play button
        private Texture2D menuPlayButtonNormal; // normal texture for play button
        private Texture2D menuQuitButtonHover;  // hover texture for quit button
        private Texture2D menuQuitButtonNormal; // normal texture for quit button
        private Button btnPlay;                 // play button
        private Button btnQuit;                 // quit button

        /// <summary>
        /// Initializes a new instance of MenuView class.
        /// </summary>
        public MenuView()
        {
        }

        /// <summary>
        /// Initializes a new instance of MenuView class.
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public MenuView(SpriteBatch spriteBatch)
        {
            Initialize(spriteBatch);
        }

        #region IView Members

        /// <summary>
        /// Initializes the view
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Initialize(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            menuBackground = AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_BG);

            menuPlayButtonHover = AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_PLAYBTNH);
            menuPlayButtonNormal = AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_PLAYBTNN);
            btnPlay = new Button(menuPlayButtonHover, menuPlayButtonNormal, spriteBatch);
            btnPlay.Location(600, 420);

            menuQuitButtonHover = AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_QUITBTNH);
            menuQuitButtonNormal = AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_QUITBTNN);
            btnQuit = new Button(menuQuitButtonHover, menuQuitButtonNormal, spriteBatch);
            btnQuit.Location(600, 470);
        }

        /// <summary>
        /// Updates the view.
        /// </summary>
        /// <param name="gameTime">the current game time</param>
        /// <param name="model">the current game model</param>
        public void Update(GameTime gameTime, IGameModel model)
        {
            btnPlay.Update();
            btnQuit.Update();
        }

        /// <summary>
        /// Draws the view.
        /// </summary>
        /// <param name="model">the current game model</param>
        public void Draw(IGameModel model)
        {
            spriteBatch.Draw(AssetsManager.Instance.GetTexture(GameAssets.ASSET_TEXTURE_BG), Vector2.Zero, Color.White);
            btnPlay.Draw();
            btnQuit.Draw();
        }

        #endregion

        /// <summary>
        /// Adds event listeners on play button click
        /// </summary>
        /// <param name="callback"></param>
        public void addEventListenerOnPlayButton(EventHandler callback)
        {
            btnPlay.OnClick += callback;
        }

        /// <summary>
        /// Adds event listeners on quit button click
        /// </summary>
        /// <param name="callback"></param>
        public void addEventListenerOnQuitButton(EventHandler callback)
        {
            btnQuit.OnClick += callback;
        }
    }
}
