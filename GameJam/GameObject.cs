using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    internal abstract class GameObject
    {
        // FIELDS
        protected Texture2D asset;
        protected Rectangle position;
        protected Rectangle sourceRectangle;
        protected int windowWidth;
        protected int windowHeight;
        protected int numberOfSprites;

        // PROPERTIES
        public int X
        {
            get { return position.X; }
            set { position.X = value; }
        }
        public int Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public int Width
        {
            get { return position.Width; }
        }

        public int Height
        {
            get { return position.Height; }
        }

        public Rectangle Bounds
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        // CONSTRUCTORS
        protected GameObject(Texture2D asset, Rectangle position, int numberOfSprites, int windowWidth, int windowHeight)
        {
            this.asset = asset;
            this.position = position;
            this.numberOfSprites = numberOfSprites;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            sourceRectangle = CalculateSourceRectangle(0, 0, asset.Width / numberOfSprites, asset.Height);
        }

        protected GameObject(Texture2D asset, Rectangle position, Rectangle sourceRectangle, int numberOfSprites, int windowWidth, int windowHeight)
        {
            this.asset = asset;
            this.position = position;
            this.sourceRectangle = sourceRectangle;
            this.numberOfSprites = numberOfSprites;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
        }

        // METHODS

        protected Rectangle CalculateSourceRectangle(int row, int column, int width, int height)
        {
            // Multiply column by width and row by height to locate correct frame 
            return new Rectangle(column * width, row * height, width, height);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            sb.Draw(
                asset,
                position,
                sourceRectangle,
                Color.White);
        }

        public abstract void Update(GameTime gameTime);
    }
}
