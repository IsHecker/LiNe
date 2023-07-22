using Save_System;
using UnityEngine;
using UnityEngine.UI;

public class SliderSave : MonoBehaviour, ISaveable
{
    private Slider _slider;
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    public object CaptureSate() => new SaveData { value = _slider.value };

    public void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        _slider.value = savedData.value;
    }

    [System.Serializable]
    struct SaveData { public float value; }
}
