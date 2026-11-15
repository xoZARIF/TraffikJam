using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameJam
{
    internal class CollisionManager
    {
        //// FIELDS
        private Player player;
        private List<Enemy> enemies;
        //Changr to back to blocks private List<Block> blocks;
        private List<GameObject> blocks;
        //// private WinObject goal;


        //change to List<GameObject> blocks??
        public CollisionManager(Player player, List<Enemy> enemies, List<GameObject> blocks)
        {
            this.player = player;
            this.enemies = enemies;
            this.blocks = blocks;
        }

        public void Update()
        {
            //// call methods of collision
            PlayerToBlocks();
            //PlayerToEnemies();
        }


        //// BASIC COLLISION METHODS

        //// Checks if two GameObjects with rectangular hitboxes are colliding with each other
        public bool RectToRect(Player player, GameObject otherObject)
        {
            // collects bounds of first rectangle
            int playerLeft = player.X;
            int playerRight = player.X + player.Width;
            int playerTop = player.Y;
            int playerBottom = player.Y + player.Height;

            // collect for 2nd
            int otherLeft = otherObject.X;
            int otherRight = otherObject.X + otherObject.Width;
            int otherTop = otherObject.Y;
            int otherBottom = otherObject.Y + otherObject.Height;

            // AABB collision check
            return playerLeft < otherRight &&
                    playerRight > otherLeft &&
                    playerTop < otherBottom &&
                    playerBottom > otherTop;
        }

        // gets rectangles for two colliding objects and finds the rectangle of their overlap
        private Rectangle CollisionSide(Player player, Block block)
        {
            Rectangle playerRect = new Rectangle(player.X, player.Y, player.Width, player.Height);
            ////For squished version
            //Rectangle otherRect = new Rectangle(player.oX, player.oY, player.oWidth, player.oHeight);

            Rectangle blockRect = new Rectangle(block.X, block.Y, block.Width, block.Height);

            //if(playerRect.Intersects(blockRect) == true)
            //{
            //    return Rectangle.Intersect(playerRect, blockRect);
            //}

            //else if(otherRect.Intersects(blockRect) == true)
            //{
            //    return Rectangle.Intersect(playerRect, blockRect);
            //}

            //placeholder?
            return Rectangle.Intersect(playerRect, blockRect); 
        }


        //// COLLISION HANDLING

        //// Handles collision b2n player & enemies
        //private void PlayerToEnemies()
        //{
        //    foreach (Enemy e in enemies)
        //    {
        //        if (RectToRect(player, e))
        //        {
        //            player.IsAlive = false; // for when PLAYER DIES, whatever lose state incorporated here
        //            break;
        //        }    
        //    }
        //}

        //// Handles collision b2n player & blocks
        private void PlayerToBlocks()
        {
            player.onGround = false;

            foreach (Block block in blocks)
            {
                if (RectToRect (player, block))
                {
                    Rectangle collisionRect = CollisionSide(player, block);

                    // vertical collisions
                    if (collisionRect.Width > collisionRect.Height)
                    {
                        // player landed on top of block
                        if (player.Y < block.Y)
                        {
                            player.onGround = true; // keep player grounded on platform
                            player.Y -= collisionRect.Height; // move player up preventing overlap
                            player.Y += 1; // move player down one pixel to keep it on ground
                        }

                        // player hits bottom of block from below
                        else
                        {
                            player.Y += collisionRect.Height; // move player down so it doesnt overlap
                        }
                    }
                    // horizontal collision
                    else
                    {
                        if (player.X < block.X)
                        {
                            player.X -= collisionRect.Width; // player hits block from the left then move back a bit
                        }
                        else
                        {
                            player.X += collisionRect.Width; // player hits block from the right
                        }
                    }
                }
            }
        }
    }
}
