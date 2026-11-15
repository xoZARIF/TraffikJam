using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

enum PlayerState
{
    Idle,
    Crouching
}

namespace GameJam
{
    internal class Player
    {
        public Texture2D idleSprite;
        protected Texture2D downSprite;
        protected Rectangle position;
        protected Rectangle otherPosition;


        PlayerState currentState = PlayerState.Crouching;

        internal Player(Texture2D spriteIdle, Texture2D spriteDown, Rectangle Iposition, Rectangle oPosition)
        {
            this.idleSprite = spriteIdle;
            this.downSprite = spriteDown;

            this.position = Iposition;
            this.otherPosition = oPosition;
        }
        

        public void Update(GameTime gameTime)
        {

            KeyboardState currState = Keyboard.GetState();

            int speed = 5;

            switch (currentState)
            {
                case PlayerState.Idle:

                    if (currState.IsKeyDown(Keys.Down))
                    {
                        currentState = PlayerState.Crouching;
                    }
                    if (currState.IsKeyDown(Keys.Up))
                    {
                        position.Y -= speed;
                        otherPosition.Y -= speed;
                    }
                    if (currState.IsKeyDown(Keys.Left))
                    {
                        position.X -= speed;
                        otherPosition.X -= speed;
                    }
                    if (currState.IsKeyDown(Keys.Right))
                    {
                        position.X += speed;
                        otherPosition.X += speed;
                    }

                    break;

                case PlayerState.Crouching:


                    if (currState.IsKeyUp(Keys.Down))
                    {
                        currentState = PlayerState.Idle;
                    }
                    if (currState.IsKeyDown(Keys.Up) || currState.IsKeyDown(Keys.Space))
                    {
                        position.Y -= speed;
                        otherPosition.Y -= speed;
                    }
                    if (currState.IsKeyDown(Keys.Left))
                    {
                        position.X -= speed;
                        otherPosition.X -= speed;
                    }
                    if (currState.IsKeyDown(Keys.Right))
                    {
                        position.X += speed;
                        otherPosition.X += speed;
                    }

                    break;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            if (currentState == PlayerState.Idle)
            {
                spriteBatch.Draw(
                    idleSprite,
                    position,
                    Color.White
                );
            }

            if (currentState == PlayerState.Crouching)
            {
                spriteBatch.Draw(
                    downSprite,
                    otherPosition,
                    Color.White
                );
            }

        }
    }
}