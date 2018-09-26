using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using Microsoft.Xna.Framework;

// https://docs.microsoft.com/en-us/dotnet/standard/io/isolated-storage
// https://github.com/SupSuper/MonoGame-SaveManager

namespace GMTB
{
    #region SaveData  
    [Serializable]
    public struct SaveData
    {
        public bool BLANK;
        public Vector2 PlayerPos;
        public string level;
        public bool Visible;
    }
    #endregion
    public class SaveLoadManager
    {
        #region Data Members
        private static SaveLoadManager Instance = null;


        private XmlSerializer serializer;
        private string mSave = "SaveFile";

        private IsolatedStorageFile isoStore;
        private string mSaveID;
        #endregion

        #region Accessors
        public string SaveID
        {
            set { mSaveID = value; }
        }
        #endregion

        #region Constructor
        private SaveLoadManager()
        {
            serializer = new XmlSerializer(typeof(SaveData));
        }
        public static SaveLoadManager getInstance
        {
            get
            {
                if (Instance == null)
                    Instance = new SaveLoadManager();
                return Instance;
            }
        }
        #endregion

        #region Methods
        public SaveData Load()
        {
            using (isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                bool FileOK = false;
                SaveData s;
                string mPath = Path.Combine(mSaveID, mSave);
                // Ignore if empty
                if (isoStore.DirectoryExists(mSaveID) && isoStore.FileExists(mPath))
                    FileOK = true;

                // Open save file
                if (FileOK)
                    using (IsolatedStorageFileStream stream = isoStore.OpenFile(mPath, FileMode.Open))
                    {
                        s = (SaveData)serializer.Deserialize(stream);
                    }
                else
                {
                    s = new SaveData();
                    s.BLANK = true;
                }
                return s;
            }
        }
        public bool Save(SaveData s)
        {
            bool IsComplete = false;
            using (isoStore = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Assembly, null, null))
            {
                // Create Directory if empty
                if (!isoStore.DirectoryExists(mSaveID))
                    isoStore.CreateDirectory(mSaveID);

                string mPath = Path.Combine(mSaveID, mSave);

                // Create Save
                using (IsolatedStorageFileStream stream = isoStore.CreateFile(mPath))
                {
                    serializer.Serialize(stream, s);
                    IsComplete = true;
                }
            }
            return IsComplete;
        }
        #endregion
    }
}
