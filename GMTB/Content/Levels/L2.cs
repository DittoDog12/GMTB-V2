using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using GMTB.AI;

namespace GMTB.Content.Levels
{
    class L2 : Level
    {
        #region Data Members
        //private string MarthaStatus;
        private int MarthaID;
        private string[] lines;
        #endregion

        #region Constructor
        public L2()
        {
            bg = "Backgrounds\\Hall1Background";
            lines = File.ReadAllLines(Environment.CurrentDirectory + "/Content/Dialogue/FirstEncounter.txt");
        }
        #endregion

        #region Methods
        public override void Initialise()
        {
            if (firstRun == true)
            {
                //Martha
                createdEntity = EntityManager.getInstance.newEntity<HighLevelAI>("Enemy/Martha/");
                SceneManager.getInstance.newEntity(createdEntity, 450, 120);
                var asInstance = createdEntity as IAI;
                asInstance.setVars(true, "Idle");
                Removables.Add(createdEntity);
                MarthaID = createdEntity.UID;

                // Door - Rushout Room
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 2) - 18, 75);
                createdEntity.setVars("L1", new Vector2(450, 250));
                Removables.Add(createdEntity);

                // Door - Matrons office
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, 290, 285);
                createdEntity.setVars("L3", new Vector2(560, ScreenHeight / 2));
                Removables.Add(createdEntity);

                // Door - south hall
                createdEntity = EntityManager.getInstance.newEntity<Door>();
                SceneManager.getInstance.newEntity(createdEntity, (ScreenWidth / 2) - 18, 400);
                createdEntity.setVars("L4", new Vector2((ScreenWidth / 4) - 10, 90));
                Removables.Add(createdEntity);

                Script.getInstance.BeginDialogue(lines);
                firstRun = false;

                //// Walls
                ////left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(280, 75), new Vector2(10, 460));
                //Walls.Add(wall);
                ////right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(505, 75), new Vector2(10, 460));
                //Walls.Add(wall);
                ////top left
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(250, 75), new Vector2(80, 10));
                //Walls.Add(wall);
                ////top right
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(360, 75), new Vector2(80, 10));
                //Walls.Add(wall);
                ////bottom
                //wall = new InvisibleWall();
                //wall.setVars(new Vector2(280, 435), new Vector2(195, 10));
                //Walls.Add(wall);

                firstRun = false;
            }
            else
            {
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
            //foreach (IEntity m in Removables)
            //    if (m.UID == MarthaID)
            //    {
            //        IAI Martha = m as IAI;
            //        MarthaStatus = Martha.State;
            //    }
            //if (MarthaStatus == "Follow")
            //{
            //    for (int i = 0; i < Removables.Count; i++)
            //        if (Removables[i].UID == MarthaID)
            //            Removables.Remove(Removables[i]);
            //}
            return Removables;
        }
        #endregion
    }
}
