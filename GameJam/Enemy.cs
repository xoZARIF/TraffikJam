using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    internal class Enemy : GameObject
    {
        // SPRITESHEET GRID

        // total # of frames, spritesheet columns and rows
        private int frameTotal = 4;
        private int cols = 2;
        private int rows = 2;


        // ANIMATION

        // current frame of the spritesheet, amount of time passed
        private int currentFrame = 0;
        private double timer = 0;
        // amount of time per frame
        private double frameTime = 0.2;


        // ENEMY MOVEMENT 

        // enemy speed
        private int speed = 2;
        // if enemy is facing right
        private bool facingRight = true;

        // ENEMY back and forth movement

        // starting x value
        private int startX;
        // end x
        private int endX;

        // constructor
        public Enemy(Texture2D asset, Rectangle position, int blockSize, int windowWidth, int windowHeight)
            : base(asset, position, 2, windowWidth, windowHeight)
        {
            // override source rectangle to match the enemy spritesheet
            int frameWidth = asset.Width / cols;
            int frameHeight = asset.Height / rows;
            sourceRectangle = CalculateSourceRectangle(0, 0, frameWidth, frameHeight);

            // save start x position
            startX = position.X;
            // calculate ending x which is the starting position plus the size of the blocktile x 4
            endX = startX + (blockSize * 4);
        }

        // update enemy state accordingly
        public override void Update(GameTime gameTime, bool debugMode)
        {
            // left and right movement
            if (facingRight)
            {
                X += speed;

                // if it reaches end x value
                if (endX <= X + Width)
                {
                    // turn around
                    facingRight = false;
                }
            }
            else
            {
                // change direction of movement if it is facing left
                X -= speed;

                // if it hits start x value
                if (X <= startX)
                {
                    // turn around agaun
                    facingRight = true;
                }
            }

            // walk cycle 

            // update time
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            // when amount of time on frame matches the timer
            if (frameTime <= timer)
            {
                // advance frame
                currentFrame++;

                // when last frame is reached
                if (frameTotal <= currentFrame)
                {
                    // go back to initial frame
                    currentFrame = 0;
                }

                // calculate enemy source rectangle
                sourceRectangle = CalculateSourceRectangle(currentFrame/cols, currentFrame%cols, asset.Width/cols, asset.Height/rows);

                // resets timer
                timer -= frameTime;
            }
        }

        public override void Draw(SpriteBatch sb)
        {
            // condition for flipping the enemy if enemy is facing right or not
            SpriteEffects flip = facingRight ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            sb.Draw(
                asset,
                position,
                sourceRectangle,
                Color.White,
                0,
                Vector2.Zero,
                flip,
                0);
        }
    }
}
