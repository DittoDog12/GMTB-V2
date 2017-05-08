using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GMTB.AI;

namespace GMTB.Content.Levels
{
    class L6 : Level
    {
        private int[] BedPositions;
        private int ItemUID;
        #region Constructor
        public L6() :base()
        {
            bg = "backgrounds\\SecondWardBackground";
            BedPositions = new int[4];
            BedPositions[0] = 150;
            BedPositions[1] = 230;
            BedPositions[2] = 300;
            BedPositions[3] = 390;

        }
        #endregion

        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                // Beds
                for (int i = 0; i< BedPositions.Length; i++)
                {
                    createdEntity = EntityManager.getInstance.newEntity<HidingPlace>("Game Items/AdultsBedSideL");
                    SceneManager.getInstance.newEntity(createdEntity, 40, BedPositions[i]);
                    Removables.Add(createdEntity);
                    createdEntity = EntityManager.getInstance.newEntity<HidingPlace>("Game Items/AdultsBedSideR");
                    SceneManager.getInstance.newEntity(createdEntity, 675, BedPositions[i]);
                    Removables.Add(createdEntity);
                }
                // Neutral AI
                createdEntity = EntityManager.getInstance.newEntity<NeutralAI>("NPC/PatientZelda/");
                SceneManager.getInstance.newEntity(createdEntity, 250, 100);
                Removables.Add(createdEntity);

                // Door - South Hallway
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 2) - 5, 90);
                createdEntity.setVars("L4", new Vector2((ScreenWidth / 4) - 10, 330));
                Removables.Add(createdEntity);

                // Item
                createdEntity = EntityManager.getInstance.newEntity<Item>("Game Items/TestTubeBottle");
                SceneManager.getInstance.newEntity(createdEntity, 200, 380);
                ItemUID = createdEntity.UID;
                Removables.Add(createdEntity);

                //// Walls
                ////Left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(25, 75), new Vector2(10, 365));
                //Walls.Add(wall);
                ////right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(740, 75), new Vector2(10, 365));
                //Walls.Add(wall);
                ////top left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(25, 75), new Vector2(370, 10));
                //Walls.Add(wall);
                ////top right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(420, 75), new Vector2(370, 10));
                //Walls.Add(wall);
                ////bottom
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(25, 440), new Vector2(715, 10));
                //Walls.Add(wall);

                firstRun = false;
            }
            else
            {
                foreach (IEntity e in EntityManager.getInstance.Entities)
                    foreach (IEntity r in Removables)
                        if (e.UID == r.UID)
                        {
                            // Check if the current entity is the Item, only spawn it if it's not been collected
                            if (r.UID == ItemUID)
                            {
                                var asInterface = r as IItem;
                                if (asInterface.Collected == false)
                                {
                                    SceneManager.getInstance.newEntity(r, (int)r.DefaultPos.X, (int)r.DefaultPos.Y);
                                    r.sub();
                                }
                                else
                                    break;

                            }
                            SceneManager.getInstance.newEntity(r, (int)r.DefaultPos.X, (int)r.DefaultPos.Y);
                            r.sub();
                        }
                foreach (IWall w in Walls)
                    w.Sub();
            }
        }
        public override List<IEntity> Exit()
        {
            return Removables;
        }
        #endregion
    }
}
