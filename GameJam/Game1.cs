using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameJam
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        // Player
        // private Player player;
        private Texture2D bubbleSprite;
        private Rectangle bubblePosition;

        // Enemy
        // private EnemyManager enemyManager;

        // Window Dimensions
        private int windowWidth;
        private int windowHeight;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            int width = GraphicsDevice.Viewport.Width;
            int height = GraphicsDevice.Viewport.Height;
            //test image loading
            bubbleSprite = Content.Load<Texture2D>("bubble-character");
            bubblePosition = new Rectangle((int)(width / 2), (int)(height / 2), (int)(bubbleSprite.Width * 0.5), (int)(bubbleSprite.Height * 0.5));

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState currState = Keyboard.GetState();

            int speed = 5;

            if (currState.IsKeyDown(Keys.Down))
            {
                bubblePosition.Y += speed;
            }
            if (currState.IsKeyDown(Keys.Up))
            {
                bubblePosition.Y -= speed;
            }
            if (currState.IsKeyDown(Keys.Left))
            {
                bubblePosition.X -= speed;
            }
            if (currState.IsKeyDown(Keys.Right))
            {
                bubblePosition.X += speed;
            }


            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _spriteBatch.Draw(bubbleSprite, bubblePosition, Color.White);

            _spriteBatch.End();


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
