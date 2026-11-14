using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    internal class LevelManager
    {
        // FIELDS
        private Texture2D backgroundAsset;
        private Texture2D tileAsset;
        private SpriteBatch _spriteBatch;
        private List<GameObject> platforms;
        private Dictionary<string, Rectangle> textureMap;
        private int windowWidth;
        private int windowHeight;
        private int spriteWidth;
        private int spriteHeight;
        private int intendedSize;

        // PROPERTY
        public List<GameObject> Platforms
        {
            get
            {
                if (platforms != null) 
                {
                    List<GameObject> platformList = platforms;
                    return platformList;
                }
                else
                {
                    return null;
                }
            }
        }

        // CONSTRUCTOR
        public LevelManager(Texture2D backgroundAsset, Texture2D tileset, SpriteBatch spriteBatch, int windowWidth, int windowHeight, string textureFile)
        {
            this.backgroundAsset = backgroundAsset;
            this.tileAsset = tileset;
            this._spriteBatch = spriteBatch;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            platforms = new List<GameObject>();
            textureMap = new Dictionary<string, Rectangle>();

            spriteWidth = 0;
            spriteHeight = 0;
            intendedSize = 0;
        }


    }
}
