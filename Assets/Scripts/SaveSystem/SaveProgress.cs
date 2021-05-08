using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaveSystem
{
    public static class SaveProgress
    {
        //Обычно использую такую систему сохранений:
        
        private static readonly string Path = Application.persistentDataPath + "/stealth.save";
        
        public static void SavePlayer()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(Path, FileMode.Create);
            SavedData savedData = new SavedData();
            formatter.Serialize(stream, savedData);
            stream.Close();
        }
        public static SavedData LoadPlayer()
        {
            if (File.Exists(Path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(Path, FileMode.Open);
                SavedData savedData = formatter.Deserialize(stream) as SavedData;
                stream.Close();
                return savedData;
            }
            Debug.LogError("Save file not found in " + Path);
            return new SavedData();
        }
    }
}
