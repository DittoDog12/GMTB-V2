using System.Collections.Generic;

using GMTB.AI;

namespace GMTB
{
    /// <summary>
    /// Main AI manager, informs AIs of Player location for follwing behaviour
    /// </summary>
    public class AiManager
    {
        #region Data Members
        private static AiManager Instance = null;
        private List<IAI> AllAIs;
        private IPlayer Player;
        private List<INeutralAI> NeutralAIs;
        private List<IEntity> Entities;
        #endregion

        #region Constructor
        private AiManager()
        {
            Entities = EntityManager.getInstance.Entities;
            AllAIs = new List<IAI>();
            
        }
        public static AiManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new AiManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void Initialise()
        {
            
        }
        public void Update()
        {
            for (int i = 0; i < Entities.Count; i++)
            {
                var asInterface = Entities[i] as IAI;
                if (asInterface != null)
                {
                    AllAIs.Add(asInterface);
                }
                if (Entities[i].UName == "Player")
                {
                    Player = Entities[i] as IPlayer;
                }
            }
            NeutralAIs = new List<INeutralAI>();
            foreach (IEntity e in Entities)
            {
                var asInterface = e as INeutralAI;
                if (asInterface != null)
                    NeutralAIs.Add(asInterface);
            }

            for (int i = 0; i < AllAIs.Count; i++)
            {
                AllAIs[i].PlayerPos = Player.Position;
                AllAIs[i].PlayerVisible = Player.Visible;
            }
            foreach (INeutralAI a in NeutralAIs)
                a.PlayerPos = Player.Position;
        }
        #endregion
    }
}
