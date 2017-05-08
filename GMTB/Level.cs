using System.Collections.Generic;

namespace GMTB.Content.Levels
{
    /// <summary>
    /// Main level class, all level descriptors inherit from this
    /// </summary>
    public abstract class Level
    {
        #region Data Members
        protected string lvlID;
        protected string bg;
        protected IEntity createdEntity;
        protected int ScreenWidth = Kernel.ScreenWidth;
        protected int ScreenHeight = Kernel.ScreenHeight;
        protected List<IEntity> Removables;
        protected List<IWall> Walls;
        protected bool firstRun = true;
        protected IWall wall;
        #endregion
        #region Accessors
        public string Background
        {
            get { return bg; }
        }
        public string LvlID
        {
            get { return lvlID; }
        }
        #endregion
        #region Constructor
        public Level()
        {
            Removables = new List<IEntity>();
            Walls = new List<IWall>();
            lvlID = GetType().Name.ToString();
        }
        #endregion
        #region Methods
        public abstract void Initialise();
        public virtual List<IEntity> Exit()
        {
            foreach (IWall w in Walls)
                w.UnSub();
            return Removables;
        }
        #endregion
    }
}
