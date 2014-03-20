using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace SSSG
{
    public enum GameAssets
    {
        ASSET_TEXTURE_EXAMPLE = 0,      //poze
        ASSET_TEXTURE_END,

        ASSET_SOUNDFX_EXAMPLE,          //efecte sonore
        ASSET_SOUNDFX_END,

        ASSET_SONG_EXAMPLE,             //cantece
        ASSET_SONG_END
    };

    public class Assets
    {
        private static Assets instance;
        private Texture2D[] graphicAssets;
        private SoundEffect[] soundFxAssets;
        private Song[] songAssets;
        private SpriteFont fontAsset;

        private Object GetAsset(int assetCounter)
        {
            if (assetCounter >= 0 && assetCounter < (int)GameAssets.ASSET_TEXTURE_END)
            {
                return graphicAssets[assetCounter];
            }
            else if (assetCounter > (int)GameAssets.ASSET_TEXTURE_END && assetCounter < (int)GameAssets.ASSET_SOUNDFX_END)
            {
                return soundFxAssets[assetCounter - (int)GameAssets.ASSET_TEXTURE_END - 1];
            }
            else if (assetCounter > (int)GameAssets.ASSET_SOUNDFX_END && assetCounter < (int)GameAssets.ASSET_SONG_END)
            {
                return songAssets[assetCounter - (int)GameAssets.ASSET_SOUNDFX_END - 1];
            }
            return fontAsset;
        }

        public Texture2D getTexture(GameAssets counter)
        {
            return (Texture2D)Assets.Instance().GetAsset((int)counter); ;
        }

        public SoundEffect getSoundFX(GameAssets counter)
        {
            return (SoundEffect)Assets.Instance().GetAsset((int)counter); ;
        }

        public Song getSong(GameAssets counter)
        {
            return (Song)Assets.Instance().GetAsset((int)counter); ;
        }

        public SpriteFont getSpriteFont()
        {
            return (SpriteFont)Assets.Instance().GetAsset(-1); ;
        }

        public void LoadGameAssets(ContentManager Content)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("DataLoading.txt");
            file.WriteLine("Asset initialization and loading in progress ...");
            graphicAssets = new Texture2D[(int)GameAssets.ASSET_TEXTURE_END];
            soundFxAssets = new SoundEffect[(int)GameAssets.ASSET_SOUNDFX_END - (int)GameAssets.ASSET_TEXTURE_END - 1];
            songAssets = new Song[(int)GameAssets.ASSET_SONG_END - (int)GameAssets.ASSET_SOUNDFX_END - 1];

            Content.RootDirectory = "Content";
            file.Write("Game font loading ...\t\t\t");
            fontAsset = Content.Load<SpriteFont>("Fonts/gameFont");
            if (fontAsset != null)
            {
                file.WriteLine("Done Succesfully");
            }
            else
            {
                file.WriteLine("Failled");
            }

            for (int i = 0; i < (int)GameAssets.ASSET_TEXTURE_END; i++)
            {
                file.Write("Game texture " + i + " ...\t\t\t");
                graphicAssets[i] = Content.Load<Texture2D>("Textures/BallLightningSmall");
                if (graphicAssets[i] != null)
                {
                    file.WriteLine("Done Succesfully");
                }
                else
                {
                    file.WriteLine("Failled");
                }
            }
            for (int i = 0; i < (int)GameAssets.ASSET_SOUNDFX_END - (int)GameAssets.ASSET_TEXTURE_END - 1; i++)
            {
                file.Write("Game soundFX " + i + " ...\t\t\t");
                soundFxAssets[i] = Content.Load<SoundEffect>("Sounds/explosion");
                if (soundFxAssets[i] != null)
                {
                    file.WriteLine("Done Succesfully");
                }
                else
                {
                    file.WriteLine("Failled");
                }
            }
            for (int i = 0; i < (int)GameAssets.ASSET_SONG_END - (int)GameAssets.ASSET_SOUNDFX_END - 1; i++)
            {
                file.Write("Game song files " + i + " ...\t\t\t");
                songAssets[i] = Content.Load<Song>("Sounds/gameMusic");
                if (songAssets[i] != null)
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

        private Assets() {}

        public static Assets Instance()
        {
            if (instance == null)
            {
                instance = new Assets();
            }
            return instance;  
        }
    }
}
