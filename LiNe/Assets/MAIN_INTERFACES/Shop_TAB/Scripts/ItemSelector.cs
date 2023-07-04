using Save_System;
using System;
using System.Collections;
using UnityEngine;

public class ItemSelector : MonoBehaviour, ISaveable
{
    const int Speed = 15;
    public void PointTo(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(Animation(target));
        IEnumerator Animation(Transform _target)
        {
            while (true)
            {
                if (Vector2.Distance(transform.localPosition, _target.localPosition) < 0.1f) yield break;
                transform.localPosition = Vector2.LerpUnclamped(transform.localPosition, _target.localPosition, Speed * Time.fixedDeltaTime);
                yield return new WaitForSeconds(0.001f);
            }
        }
    }
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