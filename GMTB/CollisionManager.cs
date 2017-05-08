using System;
using System.Collections.Generic;

namespace GMTB
{
    /// <summary>
    /// Main Collision detection manager, calls the Entities Collision Method in the event of a collision
    /// Also checks for AI colliding with walls
    /// </summary>
    public class CollisionManager
    {
        #region Data Members
        private static CollisionManager Instance = null;

        public event EventHandler<CollisionEvent> Colliders;
        public event EventHandler<CollisionEvent> PlayerE;
        // Create a Reference for all onscreen entities
        private IPlayer mPlayer;
        private List<Collidable> AllCollidables;
        private List<IWall> mWalls;
        #endregion

        #region Accessors
        public IPlayer currPlayer
        {
            get { return mPlayer; }
        }
        public List<IWall> Walls
        {
            get { return mWalls; }
            set { mWalls = value; }
        }
        #endregion

        #region Constructor
        private CollisionManager()
        {
            // Initialize AllCollidables list
            AllCollidables = new List<Collidable>();
            mWalls = new List<IWall>();
            // Initialize Player list
            //IdentifyPlayers();
            //IdentifyCollidable();
        }
        public static CollisionManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new CollisionManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        //public void IdentifyPlayers()
        //{
        //    List<IEntity> mEntities = EntityManager.getInstance.Entities;
        //    // Store the players in a second list
        //    for (int i = 0; i < mEntities.Count; i++)
        //    {
        //        var asInterface = mEntities[i] as IPlayer;
        //        if (asInterface != null)
        //            mPlayer = asInterface;
        //    }
        //}

        //public void IdentifyCollidable()
        //{
        //    List<IEntity> mEntities = EntityManager.getInstance.Entities;
        //    for (int i = 0; i < mEntities.Count; i++)
        //    {
        //        var asInterface = mEntities[i] as Collidable;
        //        if (asInterface != null)
        //            AllCollidables.Add(asInterface);
        //    }
        //}

        public void PlayerSubscribe(EventHandler<CollisionEvent> handler, IPlayer player)
        {
            PlayerE += handler;
            mPlayer = player;
        }
        public void Subscribe(EventHandler<CollisionEvent> handler, Collidable entity)
        {
            Colliders += handler;
            AllCollidables.Add(entity);
        }
        public void unSubscribe(EventHandler<CollisionEvent> handler, Collidable entity)
        {
            Colliders -= handler;
            AllCollidables.Remove(entity);
        }

        public virtual void onCollide(Collidable entity)
        {
            CollisionEvent args = new CollisionEvent(entity);
            Colliders(this, args);
        }
        public virtual void PlayerCollide(IPlayer entity)
        {
            CollisionEvent args = new CollisionEvent();
            PlayerE(this, args);
        }
        public virtual void onCollide(Collidable entity, IWall wall)
        {
            CollisionEvent args = new CollisionEvent(entity, wall);
            Colliders(this, args);
        }


        public void Update()
        {
            foreach (IWall w in Walls)
            {
                foreach (Collidable c in AllCollidables)
                    if (c.HitBox.Intersects(w.HitBox))
                        onCollide(c,w);
                if (mPlayer.HitBox.Intersects(w.HitBox))
                    PlayerCollide(mPlayer);
            }
                

            //foreach (Collidable entity in AllCollidables)
            for (int i = 0; i < AllCollidables.Count; i++)
                if (mPlayer.HitBox.Intersects(AllCollidables[i].HitBox))
                    if (AllCollidables[i].UName == "Door")
                        onCollide(AllCollidables[i]);
                    else
                    {
                        PlayerCollide(mPlayer);
                        onCollide(AllCollidables[i]);

                    }

            // Load each Collidable Object
            //for (int i = 0; i < AllCollidables.Count; i++)
            //{
            //    // Compare Loaded object with Player
            //    if (mPlayer.HitBox.Intersects(AllCollidables[i].HitBox))
            //    {
            //        // Trigger Collision methods, only trigger Collidable object if its a hiding place, else trigger object and player
            //        //if (AllCollidables[i].UName == "HidingPlace")
            //           // AllCollidables[i].Collision();
            //        //else
            //        //{
            //            AllCollidables[i].Collision();
            //            mPlayer.Collision();
            //       // }


            //    }
            //}
        }
        #endregion


    }
}
