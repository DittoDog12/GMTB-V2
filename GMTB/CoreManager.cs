using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GMTB
{
    /// <summary>
    /// Manager to control all other Managers based on GameState
    /// </summary>
    public class CoreManager
    {
        #region DataMembers
        private static CoreManager Instance = null;

        private List<AManager> AdditionalManagers;
        #endregion

        #region Constructor
        private CoreManager()
        {
            AdditionalManagers = new List<AManager>();
        }
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
        public void AddAdditionalManagers(AManager m)
        {
            // Any additional Managers can inherit from the abstract AManager class, 
            // and then use this method to reveal themselves to the core manager.
            // Remember they do not need to use the getInstance in the update loop below.
            AdditionalManagers.Add(m);
        }

        public void Update(GameTime gameTime)
        {
            SceneManager.getInstance.Update(gameTime);
            if (Global.GameState == Global.availGameStates.Playing || Global.GameState == Global.availGameStates.Dialogue)
            {      
                Script.getInstance.Update(gameTime);
                AiManager.getInstance.Update();
                ProximityManager.getInstance.Update();
                CollisionManager.getInstance.Update();
                foreach (AManager m in AdditionalManagers)
                    m.Update(gameTime);
            }
            Input.getInstance.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            SceneManager.getInstance.Draw(spriteBatch);
        }
        // To relay following camera to SM
        public void Draw(SpriteBatch spriteBatch, Camera2D cam, GraphicsDevice graDev)
        {
            SceneManager.getInstance.Draw(spriteBatch, cam, graDev);
        }
        #endregion

    }
}
