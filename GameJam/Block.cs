using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    internal class Block : GameObject
    {
        // CONSTRUCTOR
        public Block(Texture2D asset, Rectangle position, Rectangle sourceRectangle, int numberOfSprites, int windowWidth, int windowHeight)
            : base(asset, position, sourceRectangle, numberOfSprites, windowWidth, windowHeight)
        {

        }

        // METHODS
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
