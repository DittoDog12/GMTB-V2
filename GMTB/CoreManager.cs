using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GMTB
{
    /// <summary>
    /// Manager to control all other Managers based on GameState
    /// </summary>
    public class CoreManager
    {
        #region DataMembers
        private static CoreManager Instance = null;
        #endregion

        #region Constructor
        private CoreManager() { }
        public static CoreManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new CoreManager();
                return Instance;
            }
        }

        #endregion

        #region Methods
        public void Update(GameTime gameTime)
        {
            SceneManager.getInstance.Update(gameTime);
            if (Global.GameState == Global.availGameStates.Playing || Global.GameState == Global.availGameStates.Dialogue)
            {      
                Script.getInstance.Update(gameTime);
                AiManager.getInstance.Update();
                ProximityManager.getInstance.Update();
                CollisionManager.getInstance.Update();
            }
            Input.getInstance.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SceneManager.getInstance.Draw(spriteBatch);
        }

        #endregion

    }
}
