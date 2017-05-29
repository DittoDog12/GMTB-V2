using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;

namespace GMTB
{
    #region SaveData  
    public struct SaveData
    {
        public Vector2 PlayerPos;
        public string level;
        public bool Visible;
    }
    #endregion
    public class SceneManager
    {
        /// <summary>
        /// Main Scene Manager, places everything on the screen and calls Draw methods
        /// </summary>
        #region Data Members
        private static SceneManager Instance = null;

        private List<IEntity> mEntities;
        private List<IEntity> mSceneGraph;
        Microsoft.Xna.Framework.Content.ContentManager Content;

        StorageDevice device;
        string containerName = "GMTBSaveData";
        string filename = "InfirmarySave.sav";
        #endregion

        #region Accessors
        public List<IEntity> Entities
        {
            get { return mEntities; }
        }

        public List<IEntity> SceneGraph
        {
            get { return mSceneGraph; }
        }
        #endregion

        #region Constructor
        private SceneManager()
        {
            // Initialise Entity List
            mEntities = EntityManager.getInstance.Entities;
            mSceneGraph = new List<IEntity>();
            Content = Global.Content;
        }
        public static SceneManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new SceneManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        internal void InitiateLoad()
        {
            device = null;
            StorageDevice.BeginShowSelector(this.Load, null);
        }
        private void Load(IAsyncResult result)
        {
            // Open Storage Device
            device = StorageDevice.EndShowSelector(result);
            // Open Storage Container
            IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
            result.AsyncWaitHandle.WaitOne();
            StorageContainer container = device.EndOpenContainer(r);

            result.AsyncWaitHandle.Close();

            if (container.FileExists(filename))
            {
                Stream stream = container.OpenFile(filename, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                SaveData save = (SaveData)serializer.Deserialize(stream);
                stream.Close();
                container.Dispose();

                // Apply save data to world
                // Create Player and place at saved location
                IEntity createdEntity = EntityManager.getInstance.newEntity<Player>(PlayerIndex.One);
                SceneManager.getInstance.newEntity(createdEntity, (int)save.PlayerPos.X, (int)save.PlayerPos.Y);
                LevelManager.getInstance.NewLevel(save.level);

                Kernel._gameState = Kernel.GameStates.Loading;
            }


        }

        internal bool InitiateSave()
        {
            device = null;
            IAsyncResult r = StorageDevice.BeginShowSelector(Save, null);
            return r.IsCompleted;
        }

        private void Save(IAsyncResult result)
        {
            // Open Storage Device
            device = StorageDevice.EndShowSelector(result);
            if (device != null && device.IsConnected)
            {
                // Create save point
                SaveData save = new SaveData()
                {
                    PlayerPos = Global.PlayerPos,
                    level = LevelManager.getInstance.CurrentLevel.LvlID,
                    Visible = Global.Player.Visible,
                };

                // Open Storage Container
                IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
                result.AsyncWaitHandle.WaitOne();
                StorageContainer container = device.EndOpenContainer(r);

                // Delete existing save if exists
                if (container.FileExists(filename))
                    container.DeleteFile(filename);

                // Create XML Stream
                Stream stream = container.CreateFile(filename);
                // Create XML Serializer
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
                // Serialize and save to file
                serializer.Serialize(stream, save);
                // Close all files
                stream.Close();
                container.Dispose();
                result.AsyncWaitHandle.Close();
            }
        }

        public void newEntity(IEntity createdEntity, int x, int y)
        {
            // Add the new entity to the SceneManagers entity list
            //mEntities.Add(createdEntity);

            mSceneGraph.Add(createdEntity);
            // Apply the entities texture
            mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
            // Set the entities initial position
            createdEntity.setPos(x, y);
            createdEntity.DefaultPos = new Vector2(x, y);

        }
        public void Update(GameTime gameTime)
        {
            if (Kernel._gameState == Kernel.GameStates.Playing)
                mEntities.ForEach(IEntity => IEntity.Update(gameTime));
            if (Kernel._gameState == Kernel.GameStates.Paused)
                Kernel.menu2.Update(gameTime);
            if (Kernel._gameState == Kernel.GameStates.GameOver)
                Kernel.GameOverScreen.Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RoomManager.getInstance.Draw(spriteBatch);

            // Only run draw if not in Game Over state
            if (Kernel._gameState != Kernel.GameStates.GameOver)
            {
                // Update Texture path for animating entity
                mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
                // Call draw method for each Entity if entity is visible
                for (int i = 0; i < mEntities.Count; i++)
                    if (mEntities[i].Visible)
                        mEntities[i].Draw(spriteBatch);
                // Run Dialogue display if game in Dialogue State
                if (Kernel._gameState == Kernel.GameStates.Dialogue)
                    DialogueBox.getInstance.Draw(spriteBatch);
                if (Kernel._gameState == Kernel.GameStates.Paused)
                    if (Kernel.menu2 != null)
                        Kernel.menu2.Draw(spriteBatch);

            }
            else if (Kernel.GameOverScreen != null && Kernel._gameState == Kernel.GameStates.GameOver)
                Kernel.GameOverScreen.Draw(spriteBatch);

            spriteBatch.End();
        }
        #endregion
    }
}
