using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMTB
{
    public class MenuManager
    {
        #region Data Members
        private static MenuManager Instance = null;

        private MainMenu mMenu;
        private PauseMenu pMenu;
        private GameOver gMenu;
        #endregion

        #region Constructor
        private MenuManager()
        {

        }

        public static MenuManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new MenuManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public MainMenu MainMenu()
        {
            if (mMenu == null)
                mMenu = new MainMenu();

            return mMenu;
        }

        public PauseMenu PauseMenu()
        {
            if(pMenu == null)
                pMenu = new PauseMenu();

            return pMenu;
        }

        public GameOver GameOverMenu()
        {
            if (gMenu == null)
                gMenu = new GameOver();
            return gMenu;
        }
        #endregion
    }
}
