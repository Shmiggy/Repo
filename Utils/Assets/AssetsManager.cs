namespace SSSG.Utils.Assets
{
    using System;
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Media;

    public class AssetsManager
    {
        private static AssetsManager instance;
        private static Object syncRoot = new Object();

        private Texture2D[] graphicAssets;
        private SoundEffect[] soundFxAssets;
        private Song[] songAssets;
        private SpriteFont fontAsset;

        private readonly string[] texAssetsList;
        private readonly string[] soundAssetsList;
        private readonly string[] songAssetsList;

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

        public Texture2D getTexture(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as Texture2D;
        }

        public SoundEffect getSoundFX(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as SoundEffect;
        }

        public Song getSong(GameAssets counter)
        {
            return AssetsManager.Instance.GetAsset((int) counter) as Song;
        }

        public SpriteFont getSpriteFont()
        {
            return AssetsManager.Instance.GetAsset(-1) as SpriteFont;
        }

        // TODO: implement a more mature loging system, maybe log4net ??
        public void LoadGameAssets(ContentManager Content)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("DataLoading.txt");
            file.WriteLine("Asset initialization and loading in progress ...");
            graphicAssets = new Texture2D[(int) GameAssets.ASSET_TEXTURE_END];
            soundFxAssets = new SoundEffect[(int) GameAssets.ASSET_SOUNDFX_END - (int) GameAssets.ASSET_TEXTURE_END - 1];
            songAssets = new Song[(int) GameAssets.ASSET_SONG_END - (int) GameAssets.ASSET_SOUNDFX_END - 1];

            Content.RootDirectory = "Content";
            file.Write("Game font loading ...".PadRight(58));
            fontAsset = Content.Load<SpriteFont>("Fonts/gameFont");
            if ( fontAsset != null )
            {
                file.WriteLine("Done Succesfully");
            }
            else
            {
                file.WriteLine("Failled");
            }

            for ( int i = 0; i < (int) GameAssets.ASSET_TEXTURE_END; i++ )
            {
                file.Write("Game texture " + texAssetsList[i].ToString().PadRight(45));
                graphicAssets[i] = Content.Load<Texture2D>(texAssetsList[i]);
                if ( graphicAssets[i] != null )
                {
                    file.WriteLine("Done Succesfully");
                }
                else
                {
                    file.WriteLine("Failled");
                }
            }
            for ( int i = 0; i < (int) GameAssets.ASSET_SOUNDFX_END - (int) GameAssets.ASSET_TEXTURE_END - 1; i++ )
            {
                file.Write("Game soundFX " + soundAssetsList[i].ToString().PadRight(45));
                soundFxAssets[i] = Content.Load<SoundEffect>(soundAssetsList[i]);
                if ( soundFxAssets[i] != null )
                {
                    file.WriteLine("Done Succesfully");
                }
                else
                {
                    file.WriteLine("Failled");
                }
            }
            for ( int i = 0; i < (int) GameAssets.ASSET_SONG_END - (int) GameAssets.ASSET_SOUNDFX_END - 1; i++ )
            {
                file.Write("Game song files " + songAssetsList[i].ToString().PadRight(42));
                songAssets[i] = Content.Load<Song>(songAssetsList[i]);
                if ( songAssets[i] != null )
                {
                    file.WriteLine("Done Succesfully");
                }
                else
                {
                    file.WriteLine("Failled");
                }
            }
            file.WriteLine("Loading operation finished !");
            file.Close();
        }

        public void UnloadGameAssets(ContentManager Content)
        {
            Content.Unload();
        }

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
