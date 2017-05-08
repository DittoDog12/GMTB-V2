using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GMTB
{
    /// <summary>
    /// Pause menu, suspends all game logic
    /// </summary>
    public class PauseMenu
    {
        #region Data Members
        Microsoft.Xna.Framework.Content.ContentManager Content;

        public bool isSubbed = false;

        private Texture2D exitButton;
        private Vector2 exitPosition;

        private Texture2D resumeButton;
        private Vector2 resumePosition;

        MouseState mouseState;

        MouseState previousMouseState;
        #endregion

        #region Constructor
        public PauseMenu()
        {
            //Background = "Backgrounds/SpawnRoomBackground";
            Content = Global.Content;
            //RoomManager.getInstance.Room = Background;
        }
        #endregion

        #region Methods
        public void Initialize(SpriteBatch spriteBatch)
        {
            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            resumePosition = new Vector2(Kernel.ScreenWidth / 4, Kernel.ScreenHeight - 75);
            resumeButton = Content.Load<Texture2D>("resume");

            // create Exit button, position a quarter the distance across the screen from the right, near the bottom
            exitPosition = new Vector2(Kernel.ScreenWidth - (Kernel.ScreenWidth / 4), Kernel.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create save button, position it center, offset by texture width
            //saveButton = Content.Load<Texture2D>("save");
            //savePosition = new Vector2(Kernel.ScreenWidth - ((Kernel.ScreenWidth / 2)-(saveButton.Width / 2)), Kernel.ScreenHeight - 50);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(resumeButton, resumePosition, Color.White);
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            //spriteBatch.Draw(saveButton, savePosition, Color.White);
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

            //Rectangle saveRect = new Rectangle((int)savePosition.X, (int)savePosition.Y, saveButton.Width, saveButton.Height);
            Rectangle exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exitButton.Width, exitButton.Height);
            Rectangle resumeRect = new Rectangle((int)resumePosition.X, (int)resumePosition.Y, resumeButton.Width, resumeButton.Height);

            if (mouseClickedRect.Intersects(resumeRect))
            {
                Kernel._gameState = Kernel.GameStates.Loading;
            }
            else if (mouseClickedRect.Intersects(exitRect))
                Kernel._gameState = Kernel.GameStates.Exiting;
            //else if (mouseClickedRect.Intersects(saveRect))
            //    SceneManager.getInstance.Save();
        }
        public void Sub()
        {
            Input.getInstance.SubscribeExit(onEsc);
            isSubbed = true;
        }
        public void unSub()
        {
            Input.getInstance.unSubscribeExit(onEsc);
            isSubbed = false;
        }
        public void onEsc(object source, EventArgs args)
        {
            Kernel._gameState = Kernel.GameStates.Loading;
        }
        #endregion
    }
}

