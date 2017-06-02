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

        private Texture2D saveButton;
        private Vector2 savePosition;

        private bool saved = false;
        private SpriteFont mFont;
        private Vector2 TextPosition;
        private string TextDisplay;

        MouseState mouseState;

        MouseState previousMouseState;
        #endregion

        #region Constructor
        public PauseMenu()
        {
            //Background = "Backgrounds/SpawnRoomBackground";
            Content = Global.Content;
            //RoomManager.getInstance.Room = Background;
            mFont = Content.Load<SpriteFont>("HudText");
            TextPosition.X = 50;
            TextPosition.Y = Global.ScreenHeight - 50;
            TextDisplay = "Saved";

            // create Start button, position a quarter the distance across the screen from the left, near the bottom
            resumePosition = new Vector2(Global.ScreenWidth / 4, Global.ScreenHeight - 75);
            resumeButton = Content.Load<Texture2D>("resume");

            // create Exit button, position a quarter the distance across the screen from the right, near the bottom
            exitPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4), Global.ScreenHeight - 75);
            exitButton = Content.Load<Texture2D>("Exit");

            // create save button, position it center, offset by texture width
            saveButton = Content.Load<Texture2D>("save");
            savePosition = new Vector2(Global.ScreenWidth - ((Global.ScreenWidth / 2) - (saveButton.Width / 2)), Global.ScreenHeight - 50);
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(resumeButton, resumePosition, Color.White);
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            spriteBatch.Draw(saveButton, savePosition, Color.White);

            if (saved == true)
                spriteBatch.DrawString(mFont, TextDisplay, TextPosition, Color.White);

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

            Rectangle saveRect = new Rectangle((int)savePosition.X, (int)savePosition.Y, saveButton.Width, saveButton.Height);
            Rectangle exitRect = new Rectangle((int)exitPosition.X, (int)exitPosition.Y, exitButton.Width, exitButton.Height);
            Rectangle resumeRect = new Rectangle((int)resumePosition.X, (int)resumePosition.Y, resumeButton.Width, resumeButton.Height);

            if (mouseClickedRect.Intersects(resumeRect))
            {
                Global.GameState = Global.availGameStates.Loading;
                saved = false;
            }
            else if (mouseClickedRect.Intersects(exitRect))
                Global.GameState = Global.availGameStates.Exiting;
            else if (mouseClickedRect.Intersects(saveRect))
                saved = SceneManager.getInstance.InitiateSave();
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
            Global.GameState = Global.availGameStates.Loading;
            saved = false;
        }
        #endregion
    }
}

