using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

            spriteWidth = 1;
            spriteHeight = 1;
            intendedSize = 64;

            StreamReader reader = null!;

            // Texture Mapping
            try
            {
                reader = new StreamReader(textureFile);

                string line = "";
                string[] splitData = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] != '/' && line[0] != '-')
                    {
                        splitData = line.Split(',');

                        if (splitData.Length == 2)
                        {
                            spriteWidth += int.Parse(splitData[0]);
                            spriteHeight += int.Parse(splitData[1]);
                        }

                        else
                        {
                            textureMap.Add(
                                splitData[0],
                                new Rectangle(
                                    spriteWidth * int.Parse(splitData[2]),
                                    spriteHeight * int.Parse(splitData[1]),
                                    spriteWidth,
                                    spriteHeight));
                        }
                    }
                }
            }

            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }

            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

        }

        // METHODS
        public void LoadLevel(string filepath)
        {
            StreamReader reader = null;

            try
            {
                reader = new StreamReader(filepath);
                string line = "";
                string[] splitData = null;

                while ((line = reader.ReadLine()) != null)
                {
                    if (line[0] != '/' && line[0] != '-')
                    {
                        splitData = line.Split(',');

                        // Create Platform
                        platforms.Add(new Block(
                            tileAsset,
                            new Rectangle(
                                intendedSize * int.Parse(splitData[2]),
                                intendedSize * int.Parse(splitData[1]),
                                intendedSize,
                                intendedSize),
                            textureMap[splitData[0]],
                            1,
                            windowWidth,
                            windowHeight));
                    }
                }
            }

            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine(error.Message);
            }

            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public void DisplayLevel()
        {
            _spriteBatch.Draw(backgroundAsset, Vector2.Zero, Color.White);

            foreach(GameObject block in platforms)
            {
                block.Draw(_spriteBatch);
            }

        }
    }

}
