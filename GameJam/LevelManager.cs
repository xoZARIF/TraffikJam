using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        private List<Texture2D> backgroundAssets;
        private Texture2D tileAsset;
        private SpriteBatch _spriteBatch;
        private List<GameObject> platforms;
        private Dictionary<string, Rectangle> textureMap;
        private int windowWidth;
        private int windowHeight;
        private int spriteWidth;
        private int spriteHeight;
        private int intendedSize;
        private List<Rectangle> backgroundSourceRctangle;

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
        public LevelManager(List<Texture2D> backgrounds, Texture2D tileset, SpriteBatch spriteBatch, int windowWidth, int windowHeight, string textureFile)
        {
            this.backgroundAssets = backgrounds;
            this.tileAsset = tileset;
            this._spriteBatch = spriteBatch;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            platforms = new List<GameObject>();
            textureMap = new Dictionary<string, Rectangle>();

            spriteWidth = 1;
            spriteHeight = 1;
            intendedSize = 64;

            backgroundSourceRctangle = new List<Rectangle>();

            for(int r = 0; r < backgroundAssets.Count; r++)
            {
                backgroundSourceRctangle.Add(new Rectangle(0, 0, windowWidth, windowHeight));
            }

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
            for(int d = 0; d < backgroundAssets.Count; d++)
            {
                _spriteBatch.Draw(backgroundAssets[d], Vector2.Zero, backgroundSourceRctangle[d], Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f);
            }

            foreach(GameObject block in platforms)
            {
                block.Draw(_spriteBatch);
            }

        }

        public void UpdateLevel(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();

            if (currentState.IsKeyDown(Keys.Right))
            {
                for (int r = 0; r < 4; r++)
                {
                    if (backgroundSourceRctangle[r+1].X > backgroundAssets[r+1].Width - 1280)
                    {
                        backgroundSourceRctangle[r+1] = new Rectangle(backgroundAssets[r+1].Width - 1280, 0, windowWidth, windowHeight);
                    }
                    else
                    {
                        backgroundSourceRctangle[r+1] = new Rectangle(backgroundSourceRctangle[r+1].X + r+1, 0, windowWidth, windowHeight);
                    }
                }
            }

            if (currentState.IsKeyDown(Keys.Left))
            {

                for (int r = 0; r < 4; r++)
                {
                    if (backgroundSourceRctangle[r + 1].X < 0)
                    {
                        backgroundSourceRctangle[r + 1] = new Rectangle(0, 0, windowWidth, windowHeight);
                    }
                    else
                    {
                        backgroundSourceRctangle[r + 1] = new Rectangle(backgroundSourceRctangle[r + 1].X - (r + 1), 0, windowWidth, windowHeight);
                    }
                }
            }
        }
    }

}
