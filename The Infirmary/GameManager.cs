using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMTB;
using GMTB.PlatformerSubsys;
using System.IO;
using Microsoft.Xna.Framework;

namespace The_Infirmary
{
    public class GameManager : AManager
    {
        #region Data Members
        private static GameManager Instance = null;

        private IEntity createdEntity;
        private PlayerIndex PIndex;

        private int mGoal;

        private float mPlayerTimer;

        private int mLvlcounter;

        #endregion

        #region Accessors
        public int Goal
        {
            set { mGoal = value; }
        }
        #endregion

        #region Constructor
        private GameManager()
        {
            CoreManager.getInstance.AddAdditionalManagers(this);
            //sW = new StreamWriter(@"C:\Users\James\Desktop\LevelTimes.txt", append: true);
            mGoal = 800;
        }
        public static GameManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new GameManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public override void Update(GameTime gameTime)
        {
            mPlayerTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Global.PlayerPos.Y > Global.ScreenHeight)
            {
                RoomManager.getInstance.Room = "Backgrounds/GameOver";
                Global.GameState = Global.availGameStates.GameOver;
            }
            if (Global.PlayerPos.X > mGoal)
            {
                NewLevel();            
            }
        }
        public void InitializeParamaters(PlayerIndex pIndex)
        {
            PIndex = pIndex;
        }
        public void InitializeGame()
        {
            createdEntity = EntityManager.getInstance.newEntity<PlatformerPlayer>(PIndex);
            SceneManager.getInstance.newEntity(createdEntity, 50, 350);
            LevelManager.getInstance.NewLevel("L1");
            mLvlcounter = 1;
            SetTimer();
        }
        public PlatformerPlayer GetPlayer()
        {
            return createdEntity as PlatformerPlayer;
        }
        public void NewLevel()
        {
            mLvlcounter++;
            string lvl = "L" + mLvlcounter.ToString();
            LevelManager.getInstance.NewLevel(lvl);
            Global.Player.Position = new Vector2(50, 350);
            SetTimer();
        }
        public void SetTimer()
        {
            mPlayerTimer = 0;
        }
        #endregion
    }
}