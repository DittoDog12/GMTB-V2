using System.Collections.Generic;
using GMTB.AI;
using Microsoft.Xna.Framework;

namespace GMTB.Content.Levels
{
    class L1 : Level
    {
        #region Data Members

        private int[] BedPositions;
        #endregion

        #region Constructor
        public L1() : base()
        {
            bg = "Backgrounds\\SpawnRoomBackground";
            BedPositions = new int[4];
            BedPositions[0] = 160;
            BedPositions[1] = 255;
            BedPositions[2] = ScreenWidth / 2;
            BedPositions[3] = (ScreenWidth / 2) + 95;

        }
        #endregion

        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                // Old Man
                createdEntity = EntityManager.getInstance.newEntity<FriendlyAI>("NPC/OldMan/");
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 2) + 145, 160);
                Removables.Add(createdEntity);

                // Hiding Place
                for (int i = 0; i < BedPositions.Length; i++)
                {
                    createdEntity = EntityManager.getInstance.newEntity<HidingPlace>("Game Items/AdultsBedLong");
                    SceneManager.getInstance.newEntity(createdEntity, BedPositions[i], 150);
                    Removables.Add(createdEntity);
                }
                //Jumpscare dude
                createdEntity = EntityManager.getInstance.newEntity<JumpScare>("Enemy/JumpScare/");
                SceneManager.getInstance.newEntity(createdEntity, 160, 150);
                Removables.Add(createdEntity);

                // Door
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, 450, 285);
                createdEntity.setVars("L2", new Vector2((ScreenWidth / 2) - 18, 110));
                Removables.Add(createdEntity);


                //// Walls
                ////Left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(150, 150), new Vector2(10, 520));
                //Walls.Add(wall);
                ////right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(670, 150), new Vector2(10, 520));
                //Walls.Add(wall);
                ////top
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(150, 150), new Vector2(510, 10));
                //Walls.Add(wall);
                ////bottom
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(150, 315), new Vector2(510, 10));
                //Walls.Add(wall);
                firstRun = false;
            }
            else
            {
                // Check each loaded entity, if it's an entity from this level then respawn it
                foreach (IEntity e in EntityManager.getInstance.Entities)
                    foreach (IEntity r in Removables)
                        if (e.UID == r.UID)
                        {
                            SceneManager.getInstance.newEntity(r, (int)r.DefaultPos.X, (int)r.DefaultPos.Y);
                            r.sub();
                        }

                foreach (IWall w in Walls)
                    w.Sub();

            }

        }

        public override List<IEntity> Exit()
        {
            base.Exit();
            return Removables;
        }
        #endregion
    }
}
