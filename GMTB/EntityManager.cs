﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;


namespace GMTB
{
    /// <summary>
    /// Main Entity creation manager, Sets all global variables for each entity on creation, eg the UID
    /// </summary>
    public class EntityManager
    {
        #region Data Members
        private static EntityManager Instance = null;


        // Create Master List for all entities
        private List<IEntity> mEntities;
        // Create UID 
        private int UID;

        // Create Deletion List
        private List<int> mDeletions;
        #endregion

        #region Accessors
        public List<IEntity> Entities
        {
            get { return mEntities; }
            set { mEntities = value; }
        }
        #endregion

        #region Constructor
        private EntityManager()
        {
            // Initialise Lists and Content Manager Reference
            mEntities = new List<IEntity>();
            // Set UID counter to 0 for first object
            UID = 1;
            // Initialise Deletion List
            mDeletions = new List<int>();
        }
        public static EntityManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new EntityManager();

                return Instance;
            }
        }
        #endregion

        #region Methods
        public IEntity newEntity<T>() where T : IEntity, new()
        {
            // Create an entity of the type specifed by the Kernel
            IEntity createdEntity = new T();
            // Store in the list
            mEntities.Add(createdEntity);
            // Set the entities UID
            createdEntity.setVars(UID);
            // Increment the UID counter
            UID++;
            // Return the new entity to the kernel
            return createdEntity;
        }
        public IEntity newEntity<T>(string path) where T : IEntity, new()
        {
            // Create an entity of the type specifed by the Kernel
            IEntity createdEntity = new T();
            // Store in the list
            mEntities.Add(createdEntity);
            // Set the entities UID
            createdEntity.setVars(UID, path);
            // Increment the UID counter
            UID++;
            // Return the new entity to the kernel
            return createdEntity;
        }
        public IEntity newEntity<T>(PlayerIndex pPlayerNum) where T : IEntity, new()
        {
            // Same as above but allow for the Player ID for controls
            IEntity createdEntity = new T();
            mEntities.Add(createdEntity);
            createdEntity.setVars(UID, pPlayerNum);
            UID++;
            return createdEntity;
        }

        public void removeEntity(int uid)
        {
            for (int i = 0; i < mEntities.Count; i++)
            {
                if (mEntities[i] != null)
                    if (mEntities[i].UID == uid)
                    {
                        mEntities[i].Destroy();
                        //mEntities[i] = null;
                        //mEntities.RemoveAt(i);   
                    }
            }
            GC.Collect();
        }

        public void queueForDeletion(int uid)
        {
            mDeletions.Add(uid);
        }
        public void removeEntities()
        {
            foreach (int i in mDeletions)
            {
                for (int e = 0; i < mEntities.Count; e++)
                {
                    if (mEntities[e].UID == i)
                    {
                        mEntities[e] = null;
                        mEntities.RemoveAt(e);
                    }
                }
            }
            mDeletions.Clear();
            GC.Collect();
        }
        #endregion
    }
}
