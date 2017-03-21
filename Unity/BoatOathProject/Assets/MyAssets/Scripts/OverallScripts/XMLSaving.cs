using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using TroopManagement;

namespace XMLSaving
{
    public class XMLSaver
    {
        public void SaveGame(PartySaveData _playerParty, List<PartySaveData> _ai)
        {
            SaveFile file = new SaveFile(_playerParty, _ai);

            XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
            Debug.Log("Saving to: " + Application.persistentDataPath + "/_SaveGame.xml");
            FileStream stream = new FileStream(Application.persistentDataPath + "/_SaveGame.xml", FileMode.Create);
            serializer.Serialize(stream, file);
            stream.Close();
            Debug.Log("Saved Game!");
        }

        public SaveFile LoadGame()
        {
            if (File.Exists(Application.persistentDataPath + "/_SaveGame.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(SaveFile));
                FileStream stream = new FileStream(Application.persistentDataPath + "/_SaveGame.xml", FileMode.Open);
                SaveFile file = serializer.Deserialize(stream) as SaveFile;
                stream.Close();

                return file;
            }
            else
            {
                Debug.LogWarning("Savefile not found!");
                return null;
            }
        }
    }

    [System.Serializable]
    public class SaveFile
    {
        public PartySaveData playerParty;
        public List<PartySaveData> bots;

        public SaveFile(PartySaveData _playerParty, List<PartySaveData> _ai)
        {
            playerParty = _playerParty;
            bots = _ai;
        }
        public SaveFile() { }
    }

    [System.Serializable]
    public class PartySaveData
    {
        public Party partyData;
        public Vector3 partyPosition;

        public PartySaveData(Party _party, Vector3 _pos)
        {
            partyData = _party;
            partyPosition = _pos;
        }
        public PartySaveData() { }
    }
}
