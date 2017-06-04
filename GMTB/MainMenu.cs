﻿using GMTB;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Main menu, displayed on start up and triggers first level load
    /// </summary>
    public class MainMenu
    {
        #region Data Members
        Microsoft.Xna.Framework.Content.ContentManager Content;

        private Texture2D startButton;
        private Vector2 startPosition;

        private Texture2D exitButton;
        private Vector2 exitPosition;

        private Texture2D loadButton;
        private Vector2 loadPosition;

        MouseState mouseState;

        MouseState previousMouseState;

        private IEntity createdEntity;
        #endregion

        #region Constructor
        public MainMenu()
        {
            Content = Global.Content;
            RoomManager.getInstance.Room = "Backgrounds/MainMenu";
        }
        #endregion

        #region Methods
        public void Initialize(SpriteBatch spriteBatch)
        {
            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            startPosition = new Vector2(Global.ScreenWidth /4, Global.ScreenHeight - 75);
            startButton = Content.Load<Texture2D>("start");

            // create Exit button, position a quarter the distance across the screen from the right, near the bottom
            exitPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4), Global.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create load button, position it center, offset by texture width
            loadButton = Content.Load<Texture2D>("load");
            loadPosition = new Vector2(Global.ScreenWidth - ((Global.ScreenWidth / 2) - (loadButton.Width / 2)), Global.ScreenHeight - 50);
        }
        public void LoadGame()
        {
            createdEntity = EntityManager.getInstance.newEntity<Player>(PlayerIndex.One);
            SceneManager.getInstance.newEntity(createdEntity, 160, Global.ScreenHeight / 2);
            LevelManager.getInstance.NewLevel("L1");
            //Kernel._gameState = Kernel.GameStates.Playing;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RoomManager.getInstance.Draw(spriteBatch);
            spriteBatch.Draw(startButton, startPosition, Color.White);
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            spriteBatch.Draw(loadButton, loadPosition, Color.White);
            spriteBatch.End();
        }
        public void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                MouseClicked(mouseState.X, mouseState.Y);

            previousMouseState = mouseState;
        }
        public void MouseClicked(int x, int y)
        {
            // Create a Rectangle around the mouse click position
            Rectangle mouseClickedRect = new Rectangle(x, y, 10, 10);

            Rectangle startRect = new Rectangle((int)startPosition.X, (int)startPosition.Y, startButton.Width, startButton.Height);
            Rectangle exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exitButton.Width, exitButton.Height);
            Rectangle loadRect = new Rectangle((int)loadPosition.X, (int)loadPosition.Y, loadButton.Width, loadButton.Height);

            if (mouseClickedRect.Intersects(startRect))
            {
                Global.GameState = Global.availGameStates.Loading;
                LoadGame();
            }
            else if (mouseClickedRect.Intersects(exitRect))
                Global.GameState = Global.availGameStates.Exiting;
            else if (mouseClickedRect.Intersects(loadRect))
                SceneManager.getInstance.InitiateLoad();
        }
        #endregion
    }
}
