using System;
using System.Collections.Generic;

namespace GMTB
{
    /// <summary>
    /// Proximity detection manager, checks to see if the player is inside a proximity box
    /// </summary>
    public class ProximityManager
    {
        #region Data Members
        private static ProximityManager Instance = null;

        public event EventHandler<ProximityEvent> Proximity;
        // Create a Reference for all onscreen entities
        private IPlayer mPlayer;
        private List<hasProximity> AllProximity;
        #endregion


        #region Constructor
        private ProximityManager()
        {
            // Initialize AllCollidables list
            AllProximity = new List<hasProximity>();
            mPlayer = CollisionManager.getInstance.currPlayer;

        }
        public static ProximityManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new ProximityManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public void Subscribe(EventHandler<ProximityEvent> handler, hasProximity entity)
        {
            Proximity += handler;
            AllProximity.Add(entity);
        }
        public void unSubscribe(EventHandler<ProximityEvent> handler, hasProximity entity)
        {
            Proximity -= handler;
            AllProximity.Remove(entity);
        }

        public virtual void inProximity(hasProximity entity)
        {
            ProximityEvent args = new ProximityEvent(entity);
            Proximity(this, args);
        }

        public void Update()
        {
            for (int i = 0; i < AllProximity.Count; i++)
                if (mPlayer.HitBox.Intersects(AllProximity[i].ProximityBox))
                    inProximity(AllProximity[i]);
            
        }
        #endregion


    }
}
