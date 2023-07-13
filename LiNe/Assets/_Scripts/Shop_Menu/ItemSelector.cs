using Save_System;
using System;
using System.Collections;
using UnityEngine;

public class ItemSelector : MonoBehaviour, ISaveable
{
    const float Speed = 0.5f;
    public void PointTo(Transform target) => LeanTween.moveLocal(gameObject, target.localPosition, Speed).setEaseOutQuint();
    public object CaptureSate() => new SaveData { x = transform.localPosition.x, y = transform.localPosition.y };
    public void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        transform.localPosition = new Vector2(savedData.x, savedData.y);
    }

    [Serializable]
    struct SaveData
    {
        public float x;
        public float y;
    }
}