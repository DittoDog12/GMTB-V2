using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;




namespace GMTB
{
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

        //StorageDevice device;
        //string containerName = "GMTBSaveData";
        //string filename = "InfirmarySave.sav";


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
        
        #region Save/Load
        // Old system uses StorageDevice, now missing from framework as of Monogame 3.6
        // New system uses IsolatedStorage via SaveLoadManager
        #region OldSystem
        //public void InitiateLoad()
        //{
        //    device = null;
        //    StorageDevice.BeginShowSelector(this.Load, null);
        //}
        //private void Load(IAsyncResult result)
        //{
        //    // Open Storage Device
        //    device = StorageDevice.EndShowSelector(result);
        //    // Open Storage Container
        //    IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
        //    result.AsyncWaitHandle.WaitOne();
        //    StorageContainer container = device.EndOpenContainer(r);

        //    result.AsyncWaitHandle.Close();

        //    if (container.FileExists(filename))
        //    {
        //        Stream stream = container.OpenFile(filename, FileMode.Open);
        //        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        //        SaveData save = (SaveData)serializer.Deserialize(stream);
        //        stream.Close();
        //        container.Dispose();

        //        // Apply save data to world
        //        // Create Player and place at saved location
        //        IEntity createdEntity = EntityManager.getInstance.newEntity<Player>(PlayerIndex.One);
        //        SceneManager.getInstance.newEntity(createdEntity, (int)save.PlayerPos.X, (int)save.PlayerPos.Y);
        //        LevelManager.getInstance.NewLevel(save.level);

        //        Global.GameState = Global.availGameStates.Loading;
        //    }


        //}

        //public bool InitiateSave()
        //{
        //    device = null;
        //    IAsyncResult r = StorageDevice.BeginShowSelector(Save, null);
        //    return r.IsCompleted;
        //}

        //private void Save(IAsyncResult result)
        //{
        //    // Open Storage Device
        //    device = StorageDevice.EndShowSelector(result);
        //    if (device != null && device.IsConnected)
        //    {
        //        // Create save point
        //        SaveData save = new SaveData()
        //        {
        //            PlayerPos = Global.PlayerPos,
        //            level = LevelManager.getInstance.CurrentLevel.LvlID,
        //            Visible = Global.Player.Visible,
        //        };

        //        // Open Storage Container
        //        IAsyncResult r = device.BeginOpenContainer(containerName, null, null);
        //        result.AsyncWaitHandle.WaitOne();
        //        StorageContainer container = device.EndOpenContainer(r);

        //        // Delete existing save if exists
        //        if (container.FileExists(filename))
        //            container.DeleteFile(filename);

        //        // Create XML Stream
        //        Stream stream = container.CreateFile(filename);
        //        // Create XML Serializer
        //        XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
        //        // Serialize and save to file
        //        serializer.Serialize(stream, save);
        //        // Close all files
        //        stream.Close();
        //        container.Dispose();
        //        result.AsyncWaitHandle.Close();
        //    }
        //}
        #endregion
        #region NewSystem
        public bool InitiateSave()
        {
            //Create save point
            SaveData save = new SaveData()
            {
                PlayerPos = Global.PlayerPos,
                level = LevelManager.getInstance.CurrentLevel.LvlID,
                Visible = Global.Player.Visible,
            };
            bool r = SaveLoadManager.getInstance.Save(save);
            return r;
        }
        public void InitiateLoad()
        {
            SaveData save = SaveLoadManager.getInstance.Load();
            if (!save.BLANK)
            {
                IEntity createdEntity = EntityManager.getInstance.newEntity<Player>(PlayerIndex.One);
                SceneManager.getInstance.newEntity(createdEntity, (int)save.PlayerPos.X, (int)save.PlayerPos.Y);
                LevelManager.getInstance.NewLevel(save.level);
                Global.GameState = Global.availGameStates.Resuming;
            }       
        }
        #endregion
        #endregion
        #region Entity Management
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
            if (Global.GameState == Global.availGameStates.Playing)
                mEntities.ForEach(IEntity => IEntity.Update(gameTime));
            if (Global.GameState == Global.availGameStates.Paused)
                MenuManager.getInstance.PauseMenu().Update(gameTime);
            if (Global.GameState == Global.availGameStates.GameOver)
                MenuManager.getInstance.GameOverMenu().Update(gameTime);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            RoomManager.getInstance.Draw(spriteBatch);

            // Only run draw if not in Game Over state
            if (Global.GameState != Global.availGameStates.GameOver)
            {
                // Update Texture path for animating entity
                mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
                // Call draw method for each Entity if entity is visible
                for (int i = 0; i < mEntities.Count; i++)
                    if (mEntities[i].Visible)
                        mEntities[i].Draw(spriteBatch);
                // Run Dialogue display if game in Dialogue State
                if (Global.GameState == Global.availGameStates.Dialogue)
                    DialogueBox.getInstance.Draw(spriteBatch);
                if (Global.GameState == Global.availGameStates.Paused)
                    if (MenuManager.getInstance.PauseMenu() != null)
                        MenuManager.getInstance.PauseMenu().Draw(spriteBatch);

            }
            else if (MenuManager.getInstance.GameOverMenu() != null && Global.GameState == Global.availGameStates.GameOver)
                MenuManager.getInstance.GameOverMenu().Draw(spriteBatch);

            spriteBatch.End();
        }
        // For use if camera is to follow player
        public void Draw(SpriteBatch spriteBatch, Camera2D cam, GraphicsDevice graDev)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend,
                null, null, null, null, cam.GetTransform(graDev));
            RoomManager.getInstance.Draw(spriteBatch);

            // Only run draw if not in Game Over state
            if (Global.GameState != Global.availGameStates.GameOver)
            {
                // Update Texture path for animating entity
                mEntities.ForEach(IEntity => IEntity.aTexture = Content.Load<Texture2D>(IEntity.aTexturename));
                // Call draw method for each Entity if entity is visible
                for (int i = 0; i < mEntities.Count; i++)
                    if (mEntities[i].Visible)
                        mEntities[i].Draw(spriteBatch);
                // Run Dialogue display if game in Dialogue State
                if (Global.GameState == Global.availGameStates.Dialogue)
                    DialogueBox.getInstance.Draw(spriteBatch);
                if (Global.GameState == Global.availGameStates.Paused
                    && MenuManager.getInstance.PauseMenu() != null)
                    MenuManager.getInstance.PauseMenu().Draw(spriteBatch);

            }
            else if (MenuManager.getInstance.GameOverMenu() != null
                && Global.GameState == Global.availGameStates.GameOver)
                MenuManager.getInstance.GameOverMenu().Draw(spriteBatch);

            spriteBatch.End();
        }
        #endregion
        #endregion
    }
}
