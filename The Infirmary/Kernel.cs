using GMTB;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace The_Infirmary
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Kernel : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int ScreenWidth;
        public static int ScreenHeight;

        private int TotalLevels = 6;
        private string LevelPath = "The_Infirmary.Levels.L";

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

            Global.ScreenHeight = ScreenHeight;
            Global.ScreenWidth = ScreenWidth;


            Global.GameState = Global.availGameStates.Menu;
            IsMouseVisible = true;
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
            MenuManager.getInstance.MainMenu().Initialize(spriteBatch);

            // Load all levels into list, pass list to Level Manager
            List<ILevel> Levels = new List<ILevel>();

            for (int i = 0; i < TotalLevels; i++)
            {
                int lvlid = i + 1;
                string LeveltoOpen = LevelPath + lvlid;
                Type t = Type.GetType(LeveltoOpen,true);           
                ILevel lvl = Activator.CreateInstance(t) as ILevel;
                Levels.Add(lvl);
            }

            LevelManager.getInstance.InitialiseAllLevels(Levels);

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
            Global.GameState = Global.availGameStates.Paused;
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
                

            if (Global.GameState == Global.availGameStates.Exiting)
                Exit();

            // TODO: Add your update logic here

            if (Global.GameState == Global.availGameStates.Menu)
                MenuManager.getInstance.MainMenu().Update(gameTime);

            else if (Global.GameState == Global.availGameStates.Loading)
            {
                IsMouseVisible = false;

                Global.GameState = Global.availGameStates.Playing;
                Input.getInstance.SubscribeExit(onEsc);

                if (MenuManager.getInstance.PauseMenu() != null)
                    MenuManager.getInstance.PauseMenu().unSub();
            }
            else if (Global.GameState == Global.availGameStates.Paused)
            {
                IsMouseVisible = true;
                if (!MenuManager.getInstance.PauseMenu().isSubbed)
                    MenuManager.getInstance.PauseMenu().Sub();
            }
            else if (Global.GameState == Global.availGameStates.GameOver)
            {
                IsMouseVisible = true;
                Input.getInstance.unSubscribeExit(onEsc);
                MenuManager.getInstance.GameOverMenu().Initialize(spriteBatch);          
                MenuManager.getInstance.GameOverMenu().Update(gameTime);
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
            if (Global.GameState == Global.availGameStates.Menu)
                MenuManager.getInstance.MainMenu().Draw(spriteBatch);
            else
                CoreManager.getInstance.Draw(spriteBatch);
            base.Draw(gameTime);
        }

    }
}
