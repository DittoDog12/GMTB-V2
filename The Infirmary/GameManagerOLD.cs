using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMTB;
using Microsoft.Xna.Framework;

namespace The_Infirmary
{
    class GameManagerOLD : AManager
    {
        #region Data Members
        private static GameManagerOLD Instance = null;

        private IEntity createdEntity;
        private PlayerIndex PIndex;
        #endregion

        #region Constructor
        private GameManagerOLD()
        {
            CoreManager.getInstance.AddAdditionalManagers(this);
        }

        public static GameManagerOLD getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new GameManagerOLD();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime) { }

        public void InitializeParamaters(PlayerIndex pIndex)
        {
            PIndex = pIndex;
        }
        public void InitializeGame()
        {
            createdEntity = EntityManager.getInstance.newEntity<Player>(PlayerIndex.One);
            SceneManager.getInstance.newEntity(createdEntity, 160, Global.ScreenHeight / 2);
            LevelManager.getInstance.NewLevel("L1");
        }
        public Player GetPlayer()
        {
            return createdEntity as Player;
        }
        #endregion
    }
}
