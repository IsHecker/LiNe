using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Save_System
{
    public class SaveLoadSystem : MonoBehaviour
    {
        private string savePath => $"C:/Users/Mhamed/Documents/save.txt";
        public static SaveLoadSystem Instance { get; private set; }

        private void Awake() => Instance = Instance == null ? this : Instance;

        private void Start() => Load();

        [ContextMenu("Save")]
        public void Save()
        {
            var state = LoadFile();
            CaptureAllObjectStates(state);
            SaveFile(state);
        }

        [ContextMenu("Load")]
        private void Load() => RestoreAllObjectStates(LoadFile());

        private Dictionary<string, object> LoadFile()
        {
            if (!File.Exists(savePath))
                return new Dictionary<string, object>();

            using FileStream stream = File.Open(savePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream) as Dictionary<string, object>;
        }

        private void SaveFile(object state)
        {
            using FileStream stream = File.Open(savePath, FileMode.Create);
            BinaryFormatter formater = new BinaryFormatter();
            formater.Serialize(stream, state);
        }

        private void CaptureAllObjectStates(Dictionary<string, object> state)
        {
            IEnumerable<SaveableEntity> entities = FindObjectsOfType<MonoBehaviour>().OfType<SaveableEntity>();
            foreach (var saveable in entities) state[saveable.ID] = saveable.CaptureState();
        }

        private void RestoreAllObjectStates(Dictionary<string, object> state)
        {
            foreach (var saveable in FindObjectsOfType<SaveableEntity>())
                if(state.TryGetValue(saveable.ID, out object value)) saveable.RestoreState(value);
        }
    }
}