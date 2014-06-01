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
        private SpriteBatch spriteBatch;

        private Texture2D menuBackGround;
        private Texture2D menuPlayButtonHover;
        private Texture2D menuPlayButtonNormal;
        private Texture2D menuQuitButtonHover;
        private Texture2D menuQuitButtonNormal;
        private Button btnPlay;
        private Button btnQuit;

        public MenuView()
        {
        }

        public MenuView(SpriteBatch spriteBatch)
        {
            Initialize(spriteBatch);
        }

        #region IView Members

        public void Initialize(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;

            menuBackGround = AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_BG);

            menuPlayButtonHover = AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PLAYBTNH);
            menuPlayButtonNormal = AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_PLAYBTNN);
            btnPlay = new Button(menuPlayButtonHover, menuPlayButtonNormal, spriteBatch);
            btnPlay.Location(600, 420);

            menuQuitButtonHover = AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_QUITBTNH);
            menuQuitButtonNormal = AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_QUITBTNN);
            btnQuit = new Button(menuQuitButtonHover, menuQuitButtonNormal, spriteBatch);
            btnQuit.Location(600, 470);
        }

        public void Update(GameTime gameTime, IGameModel model)
        {
            btnPlay.Update();
            btnQuit.Update();
        }

        public void Draw(IGameModel model)
        {
            spriteBatch.Draw(AssetsManager.Instance.getTexture(GameAssets.ASSET_TEXTURE_BG), Vector2.Zero, Color.White);
            btnPlay.Draw();
            btnQuit.Draw();
        }

        #endregion

        public void addEventListenerOnPlayButton(EventHandler callback)
        {
            btnPlay.OnClick += callback;
        }

        public void addEventListenerOnQuitButton(EventHandler callback)
        {
            btnQuit.OnClick += callback;
        }
    }
}
