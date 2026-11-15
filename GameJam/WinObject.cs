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
        private Player player;

        public WinObject(Texture2D asset, Rectangle position, int numberOfSprites, int windowWidth, int windowHeight, Player player)
            : base(asset, position, numberOfSprites, windowWidth, windowHeight)
        {
            this.player = player;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(asset, new Vector2(position.X, position.Y), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            spriteBatch.Draw(asset, position, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = new Rectangle(player.X, player.Y, player.Width, player.Height);
            if (position.Intersects(playerRect))
            {
                player.Won = true;
            }
            //throw new NotImplementedException();
        }
    }
}
