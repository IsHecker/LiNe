using Save_System;
using UnityEngine;

public class HeadItems : MonoBehaviour, ISaveable
{
    private TrailRenderer trailColor;
    private void Awake() => trailColor = GetComponent<TrailRenderer>();

    public object CaptureSate() => new SaveData { r = trailColor.material.color.r, g = trailColor.material.color.g, b = trailColor.material.color.b };

    public void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        trailColor.material.color = new Color(savedData.r, savedData.g, savedData.b);
    }

    [System.Serializable]
    struct SaveData
    {
        public float r;
        public float g;
        public float b;
    }
}
