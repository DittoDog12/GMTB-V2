using System.Collections.Generic;

namespace GMTB
{
    /// <summary>
    /// Main level manager, controls removal of old entities and loading of new level
    /// </summary>
    public class LevelManager
    {
        #region Data Members
        private static LevelManager Instance = null;
        private Level currLevel;
        private List<IEntity> Removables;
        private List<Level> AllLoadedLevels;
        private List<Level> AllLevels;
        private bool firstRun = true;
        #endregion

        #region Accessors
        public Level CurrentLevel
        {
            get { return currLevel; }
            set { currLevel = value; }
        }

        public List<Level> AllLvls
        {
            get { return AllLoadedLevels; }
            set { AllLoadedLevels = value; }
        }
        #endregion

        #region Constructor
        private LevelManager()
        {
            Removables = new List<IEntity>();
            AllLoadedLevels = new List<Level>();
        }
        public static LevelManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new LevelManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void InitialiseAllLevels(List<Level> Levels)
        {
            AllLevels = new List<Level>();
            foreach (Level l in Levels)
                AllLevels.Add(l);
        }


        public void NewLevel(string LevelID)
        {
            // Clean up old level
            if (currLevel != null)
            {
                Removables = currLevel.Exit();
                foreach (IEntity e in Removables)
                    EntityManager.getInstance.removeEntity(e.UID);
            }
            if (firstRun == true)
            {
                foreach (Level l in AllLevels)
                    if (l.LvlID == LevelID)
                        currLevel = l;

                AllLoadedLevels.Add(currLevel);
                firstRun = false;
            }
            else
            {
                // Check if level has already been loaded
                bool newLevel = true;
                for (int i = 0; i < AllLoadedLevels.Count; i++)
                {
                    if (AllLoadedLevels[i].LvlID == LevelID)
                    {
                        currLevel = AllLoadedLevels[i];
                        newLevel = false;
                    }
                }

                if (newLevel == true)
                {
                    foreach (Level l in AllLevels)
                        if (l.LvlID == LevelID)
                            currLevel = l;

                    AllLoadedLevels.Add(currLevel);
                }
            }
            currLevel.Initialise();
            LoadBackground(currLevel.Background);

        }

        private void LoadBackground(string LevelBG)
        {
            RoomManager.getInstance.Room = LevelBG;
        }
        #endregion
    }
}
