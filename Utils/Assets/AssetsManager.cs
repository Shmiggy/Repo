namespace SSSG.Utils.Assets
{
    using System;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public class AssetsManager
    {
        #region log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #endregion

        private static AssetsManager instance;          // the AssetsManager instance
        private static Object syncRoot = new Object();  // dummy object, used to lock access

        private Texture2D[] graphicAssets;              // graphic assets
        private SoundEffect[] soundFxAssets;            // sound effects assets
        private Song[] songAssets;                      // music assets
        private SpriteFont fontAsset;                   // font asset

        private readonly string[] texAssetsList;        // textures paths
        private readonly string[] soundAssetsList;      // sounds paths
        private readonly string[] songAssetsList;       // songs paths

        /// <summary>
        /// Initializes a new instance of AssetsManager class.
        /// </summary>
        private AssetsManager()
        {
            texAssetsList = new string[] { 
                "Textures/Rocket", 
                "Textures/Reaper", 
                "Textures/Stars", 
                "Textures/Mine", 
                "Textures/Projectile", 
                "Textures/Explosion", 
                "Textures/MenuScreen", 
                "Textures/PlayButton", 
                "Textures/PlayButtonHover", 
                "Textures/QuitButton", 
                "Textures/QuitButtonHover" 
            };

            soundAssetsList = new string[] { 
                "Sounds/explosion"
            };

            songAssetsList = new string[] { 
                "Sounds/gameMusic"
            };
        }

        /// <summary>
        /// Gets a particular asset.
        /// </summary>
        /// <param name="assetCounter">which asset to retrieve</param>
        /// <returns>the found asset</returns>
        private Object GetAsset(int assetCounter)
        {
            if ( assetCounter >= 0 && assetCounter < (int) GameAssets.ASSET_TEXTURE_END )
            {
                return graphicAssets[assetCounter];
            }
            else if ( assetCounter > (int) GameAssets.ASSET_TEXTURE_END && assetCounter < (int) GameAssets.ASSET_SOUNDFX_END )
            {
                return soundFxAssets[assetCounter - (int) GameAssets.ASSET_TEXTURE_END - 1];
            }
            else if ( assetCounter > (int) GameAssets.ASSET_SOUNDFX_END && assetCounter < (int) GameAssets.ASSET_SONG_END )
            {
                return songAssets[assetCounter - (int) GameAssets.ASSET_SOUNDFX_END - 1];
            }
            return fontAsset;
        }

        /// <summary>
        /// Gets the desired texture.
        /// </summary>
        /// <param name="counter">which texture to retrieve</param>
        /// <returns>the found texture</returns>
        public Texture2D GetTexture(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as Texture2D;
        }

        /// <summary>
        /// Gets the desired sound effect.
        /// </summary>
        /// <param name="counter">which sound effect to retrieve</param>
        /// <returns>the found sound effect</returns>
        public SoundEffect GetSoundFX(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as SoundEffect;
        }

        /// <summary>
        /// Gets the desired song.
        /// </summary>
        /// <param name="counter">which song to retrieve</param>
        /// <returns>the found song</returns>
        public Song GetSong(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as Song;
        }

        /// <summary>
        /// Gets the font sprite.
        /// </summary>
        /// <returns>the font sprite</returns>
        public SpriteFont GetSpriteFont()
        {
            return AssetsManager.Instance.GetAsset(-1) as SpriteFont;
        }

        /// <summary>
        /// Loads the game assets
        /// </summary>
        /// <param name="Content">the content</param>
        public void LoadGameAssets(ContentManager Content)
        {
            log.Info("Asset initialization and loading in progress ...");

            graphicAssets = new Texture2D[(int) GameAssets.ASSET_TEXTURE_END];
            soundFxAssets = new SoundEffect[(int) GameAssets.ASSET_SOUNDFX_END - (int) GameAssets.ASSET_TEXTURE_END - 1];
            songAssets = new Song[(int) GameAssets.ASSET_SONG_END - (int) GameAssets.ASSET_SOUNDFX_END - 1];

            Content.RootDirectory = "Content";

            log.Info("Game font loading ...".PadRight(58));

            fontAsset = Content.Load<SpriteFont>("Fonts/gameFont");
            if ( fontAsset != null )
            {
                log.Info("Done Succesfully");
            }
            else
            {
                log.Warn("Failled");
            }

            for ( int i = 0; i < (int) GameAssets.ASSET_TEXTURE_END; i++ )
            {
                log.Info("Game texture " + texAssetsList[i].ToString().PadRight(45));

                graphicAssets[i] = Content.Load<Texture2D>(texAssetsList[i]);
                if ( graphicAssets[i] != null )
                {
                    log.Info("Done Succesfully");
                }
                else
                {
                    log.Warn("Failled");
                }
            }
            for ( int i = 0; i < (int) GameAssets.ASSET_SOUNDFX_END - (int) GameAssets.ASSET_TEXTURE_END - 1; i++ )
            {
                log.Info("Game soundFX " + soundAssetsList[i].ToString().PadRight(45));
                soundFxAssets[i] = Content.Load<SoundEffect>(soundAssetsList[i]);
                if ( soundFxAssets[i] != null )
                {
                    log.Info("Done Succesfully");
                }
                else
                {
                    log.Warn("Failled");
                }
            }
            for ( int i = 0; i < (int) GameAssets.ASSET_SONG_END - (int) GameAssets.ASSET_SOUNDFX_END - 1; i++ )
            {
                log.Info("Game song files " + songAssetsList[i].ToString().PadRight(42));
                songAssets[i] = Content.Load<Song>(songAssetsList[i]);
                if ( songAssets[i] != null )
                {
                    log.Info("Done Succesfully");
                }
                else
                {
                    log.Warn("Failled");
                }
            }
            log.Info("Loading operation finished !");
        }

        /// <summary>
        /// Unloads the game assets
        /// </summary>
        /// <param name="Content">the content</param>
        public void UnloadGameAssets(ContentManager Content)
        {
            Content.Unload();
        }

        /// <summary>
        /// Gets the AssetsManager instance.
        /// </summary>
        public static AssetsManager Instance
        {
            get
            {
                if ( instance == null )
                {
                    lock ( syncRoot )
                    {
                        if ( instance == null )
                        {
                            instance = new AssetsManager();
                        }
                    }
                }

                return instance;
            }
        }

    }
}
