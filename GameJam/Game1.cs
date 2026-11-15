using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GameJam
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Player
        // private Player player;
        private Player player;
        private Texture2D bubbleSprite;
        private Texture2D downSprite;
        private Rectangle idlePosition;
        private Rectangle downPosition;
        // Enemy
        private EnemyManager enemyManager;
        private Texture2D cactusSprite;

        // Window Dimensions
        private int windowWidth;
        private int windowHeight;

        // Level
        private LevelManager levelManager;

        private List<Texture2D> backgroundLayers;
        private Texture2D background;
        private Texture2D tileset;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // Changes the size of the game window
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 512;
            _graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            windowWidth = GraphicsDevice.Viewport.Width;
            windowHeight = GraphicsDevice.Viewport.Height;
            backgroundLayers = new List<Texture2D>();

            base.Initialize();
        }
      
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            //test image loading

            bubbleSprite = Content.Load<Texture2D>("bubble-character");
            downSprite = Content.Load<Texture2D>("squished-bubble");
            idlePosition = new Rectangle((int)(width / 2), (int)(height / 2), (int)(bubbleSprite.Width * 0.5), (int)(bubbleSprite.Height * 0.5));
            downPosition = new Rectangle((int)(width / 2), (int)(height / 2 + (int)(downSprite.Width * 0.25)), (int)(downSprite.Width * 0.5), (int)(downSprite.Height * 0.5));

            player = new Player(bubbleSprite, downSprite, idlePosition, downPosition);


            background = Content.Load<Texture2D>("CanyonDesert-BGStatic");
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGStatic"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGlayer1"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGSlayer2"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGlayer3"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGlayer4"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGlayer5"));
            backgroundLayers.Add(Content.Load<Texture2D>("CanyonDesert-BGlayer6"));
            tileset = Content.Load<Texture2D>("tilesheetEDIT");

            levelManager = new LevelManager(
                background,
                tileset,
                _spriteBatch,
                windowWidth,
                windowHeight,
                "../../../Content/gameJamTextureMapping.txt");
            levelManager.LoadLevel("../../../Content/gameJamLevelFile.txt");

            // TODO: use this.Content to load your game content here

            // Load cactus spritesheet
            cactusSprite = Content.Load<Texture2D>("cacto run");

            // create enemy and add to manager
            Enemy enemy = new Enemy(
                cactusSprite,
                new Rectangle((int)(width / 2), (int)(height / 2), (int)(bubbleSprite.Width * 0.8), (int)(bubbleSprite.Height * 0.8)), // starting pos placeholder
                30, // block size placeholder
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height);

            // initialize enemy manager
            enemyManager = new EnemyManager();
            enemyManager.AddEnemy(enemy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState currState = Keyboard.GetState();

            player.Update(gameTime);
            //int speed = 5;

            //if (currState.IsKeyDown(Keys.Down))
            //{
            //    bubblePosition.Y += speed;
            //}
            //if (currState.IsKeyDown(Keys.Up))
            //{
            //    bubblePosition.Y -= speed;
            //}
            //if (currState.IsKeyDown(Keys.Left))
            //{
            //    bubblePosition.X -= speed;
            //}
            //if (currState.IsKeyDown(Keys.Right))
            //{
            //    bubblePosition.X += speed;
            //}


            // TODO: Add your update logic here

            // update enemy
            enemyManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            levelManager.DisplayLevel();

            //_spriteBatch.Draw(bubbleSprite, bubblePosition, Color.White);
            player.Draw(_spriteBatch);

            // draw enemy
            enemyManager.Draw(_spriteBatch);

            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
