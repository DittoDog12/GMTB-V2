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

        private Texture2D AButton;
        private Vector2 APosition;

        private Texture2D BButton;
        private Vector2 BPosition;

        private Texture2D XButton;
        private Vector2 XPosition;
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

            // Create A, B and X Buttons, Will only render if controller connected, allows for on the fly adjustment
            APosition = new Vector2((Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            AButton = Content.Load<Texture2D>("A-Button");

            BPosition = new Vector2(Global.ScreenWidth - (Global.ScreenWidth / 4) + 30, Global.ScreenHeight - 100);
            BButton = Content.Load<Texture2D>("B-Button");

            XPosition = new Vector2(Global.ScreenWidth - ((Global.ScreenWidth / 2) - (saveButton.Width / 2)) + 30, Global.ScreenHeight - 100);
            XButton = Content.Load<Texture2D>("X-Button");
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(resumeButton, resumePosition, Color.White);
            spriteBatch.Draw(exitButton, exitPosition, Color.White);
            spriteBatch.Draw(saveButton, savePosition, Color.White);
            if (Input.getInstance.CheckController())
            {
                spriteBatch.Draw(AButton, APosition, Color.White);
                spriteBatch.Draw(BButton, BPosition, Color.White);
                spriteBatch.Draw(XButton, XPosition, Color.White);
            }

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
                Resume();
            else if (mouseClickedRect.Intersects(exitRect))
                Exit();
            else if (mouseClickedRect.Intersects(saveRect))
                Save();
        }
        public void Sub()
        {
            Input.getInstance.SubscribeExit(onEsc);
            Input.getInstance.SubscribeGPMenu(GP);
            isSubbed = true;
        }
        public void unSub()
        {
            Input.getInstance.unSubscribeExit(onEsc);
            Input.getInstance.UnSubscribeGPMenu(GP);
            isSubbed = false;
        }
        public void onEsc(object source, EventArgs args)
        {
            Resume();
        }
        public void GP(object source, GPEvent args)
        {
            if (args.currentState == "A")
                Resume();
            if (args.currentState == "B")
                Exit();
            if (args.currentState == "X")
                Exit();
        }

        public void Resume()
        {
            Global.GameState = Global.availGameStates.Resuming;
            saved = false;
        }
        public void Exit()
        {
            Global.GameState = Global.availGameStates.Exiting;
        }
        public void Save()
        {
            saved = SceneManager.getInstance.InitiateSave();
        }
        #endregion
    }
}

