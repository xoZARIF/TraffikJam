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

enum GravityState
{
    Grounded,
    InAir
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
        GravityState gravity = GravityState.Grounded;

        public int X
        {
            get { return position.X; }
            set { position.X = value; }
        }

        public int oX
        {
            get { return otherPosition.X; }
            set { otherPosition.X = value; }
        }
        public int Y
        {
            get { return position.Y; }
            set { position.Y = value; }
        }

        public int oY
        {
            get { return otherPosition.Y; }
            set { otherPosition.Y = value; }
        }

        public int Width
        {
            get { return position.Width; }
            set { position.Width = value; }
        }

        public int oWidth
        {
            get { return otherPosition.Width; }
            set { otherPosition.Width = value; }
        }

        public int Height
        {
            get { return position.Height; }
            set { position.Height = value; }
        }

        public int oHeight
        {
            get { return otherPosition.Height; }
            set { otherPosition.Height = value; }
        }

        public bool onGround
        {
            //Questionable
            get { return onGround; }
            set { if (gravity == GravityState.Grounded)
                    onGround = false;
                else onGround = true;
                }
                }

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
            int fallGravity = 3;

            //Player Movement
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

            //Player Movement

            switch (gravity)
            {
                case GravityState.Grounded:

                    if (currState.IsKeyDown(Keys.Up))
                    {
                        gravity = GravityState.InAir;
                    }
                    
                    break;

                case GravityState.InAir:
                    position.Y += fallGravity;
                    otherPosition.Y += fallGravity;
                    {
                        break;
                    }

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