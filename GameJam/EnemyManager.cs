using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameJam
{
    internal class EnemyManager
    {
        // PROPERTY
        public List<Enemy> Enemies { get; private set; }

        // CONSTRUCTOR
        public EnemyManager()
        {
            // instantiate a new list
            Enemies = new List<Enemy>();
        }

        // METHODS

        // add enemy to list
        public void AddEnemy(Enemy enemy)
        {
            if (enemy != null)
            {
                Enemies.Add(enemy);
            }    
        }

        // update enemy per frame
        public void Update(GameTime gameTime)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Update(gameTime);
            }
        }

        // draws all enemies
        public void Draw(SpriteBatch sb)
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw(sb);
            }
        }

        // clears all existing enemies
        public void Clear()
        {
            Enemies.Clear();
        }
    }
}
