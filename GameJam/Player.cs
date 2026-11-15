using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        protected Texture2D idleSprite;
        protected Texture2D downSprite;
        protected Rectangle position;

        PlayerState currentState = PlayerState.Idle;

        internal Player(Texture2D idleSprite, Texture2D downSprite, Rectangle dimmensions)
        {

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
                    }
                    if (currState.IsKeyDown(Keys.Left))
                    {
                        position.X -= speed;
                    }
                    if (currState.IsKeyDown(Keys.Right))
                    {
                        position.X += speed;
                    }
                    break;

                case PlayerState.Crouching:

                    if (currState.IsKeyDown(Keys.Right))
                    {
                        currentState = PlayerState.Idle;
                    }
                    if (currState.IsKeyDown(Keys.Up))
                    {
                        currentState = PlayerState.Idle;
                    }
                    if (currState.IsKeyDown(Keys.Left))
                    {
                        currentState = PlayerState.Idle;
                    }

                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(
                idleSprite,
                position,
                Color.White
            );

            spriteBatch.Draw(
                downSprite,
                position,
                Color.White
            );

        }
    }
}