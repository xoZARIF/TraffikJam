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
        private List<Rectangle> backgroundSourceRectangles;
        private Vector2 worldPosition;
        private Vector2 worldToScreen;
        private List<Rectangle> originalPositions;
        private WinObject winObject;
        private Vector2 winObjectOriginalPosition;

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
        public LevelManager(List<Texture2D> backgrounds, Texture2D tileset, SpriteBatch spriteBatch, int windowWidth, int windowHeight, string textureFile, WinObject winObject)
        {
            this.backgroundAssets = backgrounds;
            this.tileAsset = tileset;
            this._spriteBatch = spriteBatch;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            this.winObject = winObject;

            platforms = new List<GameObject>();
            textureMap = new Dictionary<string, Rectangle>();

            spriteWidth = 1;
            spriteHeight = 1;
            intendedSize = 64;

            backgroundSourceRectangles = new List<Rectangle>();

            worldPosition = Vector2.Zero;
            worldToScreen = Vector2.Zero;
            originalPositions = new List<Rectangle>();

            winObjectOriginalPosition = new Vector2(winObject.X, winObject.Y);

            for(int r = 0; r < backgroundAssets.Count; r++)
            {
                backgroundSourceRectangles.Add(new Rectangle(0, 0, windowWidth, windowHeight));
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

            for (int o = 0; o < platforms.Count; o++)
            {
                originalPositions.Add(new Rectangle(platforms[o].X, platforms[o].Y, intendedSize, intendedSize));
            }
        }

        public void DisplayLevel()
        {
            for(int d = 0; d < backgroundAssets.Count; d++)
            {
                _spriteBatch.Draw(
                    backgroundAssets[d], 
                    Vector2.Zero, 
                    backgroundSourceRectangles[d], 
                    Color.White, 
                    0f, 
                    Vector2.Zero, 
                    2f, 
                    SpriteEffects.None, 
                    0f);
            }

            foreach(GameObject block in platforms)
            {
                block.Draw(_spriteBatch);
            }

            winObject.Draw(_spriteBatch);

        }

        public void UpdateLevel(GameTime gameTime, Vector2 playerPosition)
        {
            worldToScreen = playerPosition - worldPosition;

            KeyboardState currentState = Keyboard.GetState();


            if (worldPosition.X < 0)
            {
                worldPosition.X = 0;

                foreach (GameObject block in platforms)
                {
                    block.X = originalPositions[platforms.IndexOf(block)].X;
                }

                winObject.X = (int)winObjectOriginalPosition.X;
                winObject.Y = (int)winObjectOriginalPosition.Y;
            }
            
            if (worldPosition.X > backgroundAssets[1].Width - 1280)
            {
                worldPosition.X = backgroundAssets[1].Width - 1280;

                foreach (GameObject block in platforms)
                {
                  // block.X = originalPositions[platforms.IndexOf(block)].X;
                }
            }

            if (currentState.IsKeyDown(Keys.Right))
            {
                //if (worldPosition.X < (backgroundAssets[1].Width - 1280))
                {
                    worldPosition.X += 5;
                    winObject.X -= 6;

                    for (int r = 0; r < 4; r++)
                    {
                        if (backgroundSourceRectangles[r + 1].X > backgroundAssets[r + 1].Width - 1280)
                        {
                            backgroundSourceRectangles[r + 1] = new Rectangle(backgroundAssets[r + 1].Width - 1280, 0, windowWidth, windowHeight);
                        }
                        else
                        {
                            backgroundSourceRectangles[r + 1] = new Rectangle(backgroundSourceRectangles[r + 1].X + r + 1, 0, windowWidth, windowHeight);
                        }
                    }

                    foreach (GameObject block in platforms)
                    {
                        block.X -= 6;
                    }
                }
            }

            if (currentState.IsKeyDown(Keys.Left))
            {
                if (worldPosition.X > 0)
                {
                    worldPosition.X -= 5;
                    winObject.X += 6;

                    for (int r = 0; r < 4; r++)
                    {
                        if (backgroundSourceRectangles[r + 1].X < 0)
                        {
                            backgroundSourceRectangles[r + 1] = new Rectangle(0, 0, windowWidth, windowHeight);
                        }
                        else
                        {
                            backgroundSourceRectangles[r + 1] = new Rectangle(backgroundSourceRectangles[r + 1].X - (r + 1), 0, windowWidth, windowHeight);
                        }
                    }

                    foreach (GameObject block in platforms)
                    {
                        block.X += 6;
                    }
                }
            }

            if (currentState.IsKeyDown(Keys.Up))
            {
                worldPosition.Y -= 5;
            }
        }


        public void ResetLevel()
        {
            worldPosition = Vector2.Zero;
            worldToScreen = Vector2.Zero;

            for (int r = 0; r < backgroundSourceRectangles.Count; r++)
            {
                backgroundSourceRectangles[r] = new Rectangle(0, 0, windowWidth, windowHeight);
            }

            for (int i = 0; i < platforms.Count; i++)
            {
                platforms[i].X = originalPositions[i].X;
                platforms[i].Y = originalPositions[i].Y;
            }
        }  
    }

}
