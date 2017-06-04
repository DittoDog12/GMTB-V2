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
        private ILevel currLevel;
        private List<IEntity> Removables;
        private List<ILevel> AllLoadedLevels;
        private List<ILevel> AllLevels;
        private bool firstRun = true;
        #endregion

        #region Accessors
        public ILevel CurrentLevel
        {
            get { return currLevel; }
        }
        #endregion

        #region Constructor
        private LevelManager()
        {
            Removables = new List<IEntity>();
            AllLoadedLevels = new List<ILevel>();
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
        public void InitialiseAllLevels(List<ILevel> Levels)
        {
            AllLevels = new List<ILevel>();
            foreach (ILevel l in Levels)
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
                foreach (ILevel l in AllLevels)
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
                    foreach (ILevel l in AllLevels)
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

        public void NewGameReset()
        {
            foreach (ILevel l in AllLevels)
                l.FirstRun = true;
            AllLoadedLevels.Clear();
            firstRun = true;
            currLevel = null;
        }
        #endregion
    }
}
