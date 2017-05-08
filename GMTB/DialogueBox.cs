using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Method for drawing text on screen, holds the position and SpriteFont to use
    /// </summary>
    public class DialogueBox
    {
        #region Data Members
        private static DialogueBox Instance = null; 

        Microsoft.Xna.Framework.Content.ContentManager Content;
        // Create SpriteFont
        private SpriteFont mFont;
        private Vector2 mPosition;
        private string mDisplay;
        private bool mActive;
        #endregion

        #region Constructors
        private DialogueBox()
        {  
            Content = Global.Content;
            mFont = Content.Load<SpriteFont>("HudText");
            mPosition.X = 50;
            mPosition.Y = Kernel.ScreenHeight - 50;
        }
        #endregion

        #region Singleton Instantisiator
        public static DialogueBox getInstance
        {
            get 
            {
                if (Instance == null)
                    Instance = new DialogueBox();
                return Instance;
            }
        }
        #endregion

        #region Methods

        public void Draw(SpriteBatch spriteBatch)
        {
            if(mActive)
            {
                spriteBatch.DrawString(mFont, mDisplay, mPosition, Color.White);
               // Debug.WriteLine("Dialogue: " + mDisplay);
                mActive = false;
            }
            
        }
        public void Display(string Display)
        {
            mDisplay = Display;
            mActive = true;
        }

        #endregion
    }
}
