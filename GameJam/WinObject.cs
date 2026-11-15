using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{

    internal class WinObject : GameObject
    {
        // private Player player;

        public WinObject(Texture2D asset, Rectangle position, int numberOfSprites, int windowWidth, int windowHeight)
            : base(asset, position, numberOfSprites, windowWidth, windowHeight)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(asset, new Vector2(position.X, position.Y), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }
    }
}
