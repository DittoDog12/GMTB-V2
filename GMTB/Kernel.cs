using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        public enum GameStates
        {
            Menu,
            Playing,
            GameOver,
            Dialogue,
            Loading,
            Exiting,
            Paused
        }
        public static GameStates _gameState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;


        // Create empty IEntity object to hold entities during creation
        //private IEntity createdEntity;

        // Create a Menu Objects
        public static MainMenu menu1;
        public static PauseMenu menu2;
        public static GameOver GameOverScreen;

        public Kernel()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Global.Content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ScreenHeight = GraphicsDevice.Viewport.Height;
            ScreenWidth = GraphicsDevice.Viewport.Width;

            _gameState = GameStates.Menu;
            IsMouseVisible = true;
            menu1 = new MainMenu();
            // Initialize Entity and Scene Managers          
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here           
            menu1.Initialize(spriteBatch);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void onEsc(object source, InputEvent args)
        {
            _gameState = GameStates.Paused;
            Input.getInstance.unSubscribeExit(onEsc);
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                

            if (_gameState == GameStates.Exiting)
                Exit();

            // TODO: Add your update logic here

            if (_gameState == GameStates.Menu)
                menu1.Update(gameTime);
            else if (_gameState == GameStates.Loading)
            {
                IsMouseVisible = false;
                _gameState = GameStates.Playing;
                Input.getInstance.SubscribeExit(onEsc);
                if (menu2 != null)
                    menu2.unSub();
            }
            else if (_gameState == GameStates.Paused)
            {
                IsMouseVisible = true;
                if (menu2 == null)
                {
                    menu2 = new PauseMenu();
                    menu2.Initialize(spriteBatch);
                }
                if (!menu2.isSubbed)
                    menu2.Sub();
            }
            else if (_gameState == GameStates.GameOver)
            {
                IsMouseVisible = true;
                Input.getInstance.unSubscribeExit(onEsc);
                if (GameOverScreen == null)
                {
                    GameOverScreen = new GameOver();
                    GameOverScreen.Initialize(spriteBatch);
                }            
                GameOverScreen.Update(gameTime);
            }
                
            CoreManager.getInstance.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (_gameState == GameStates.Menu)
                menu1.Draw(spriteBatch);
            else
                CoreManager.getInstance.Draw(spriteBatch);
            base.Draw(gameTime);
        }

    }
}
