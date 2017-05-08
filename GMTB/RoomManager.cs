using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Controls background drawing
    /// </summary>
    class RoomManager
    {
        #region Data Members
        private static RoomManager Instance = null;
        private string mRoom;
        private Texture2D Background;
        Microsoft.Xna.Framework.Content.ContentManager Content;
        #endregion

        #region Accessors
        public string Room
        {
            get { return mRoom; }
            set { mRoom = value; }
        }
        #endregion

        #region Constructor
        private RoomManager()
        {
            Content = Global.Content;
        }
        public static RoomManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new RoomManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            Background = Content.Load<Texture2D>(mRoom);
            spriteBatch.Draw(Background, new Rectangle(0, 0, 800, 480), Color.White);
        }
        #endregion
    }
}
