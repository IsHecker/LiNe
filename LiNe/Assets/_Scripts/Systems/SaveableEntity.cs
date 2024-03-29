using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Save_System
{
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string id = string.Empty;

        public string ID => id;

        [ContextMenu("Generate ID")]
        private void GenerateID() => id = Guid.NewGuid().ToString();


        public object CaptureState()
        {
            var state = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
                state[saveable.GetType().Name] = saveable.CaptureSate();

            return state;
        }

        public void RestoreState(object state)
        {
            var stateDictionary = (Dictionary<string, object>)state;
            foreach (var saveable in GetComponents<ISaveable>())
                if (stateDictionary.TryGetValue(saveable.GetType().Name, out object value)) saveable.RestoreState(value);
        }
    }
}
