using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Save_System;

[RequireComponent(typeof(SaveableEntity))]
public class SwitchBehaviour : MonoBehaviour, ISaveable
{
    private bool isTurnedOn = true;
    private TMP_Text buttonText;
    private Image buttonImage;

    private void Awake()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void SwitchOnOff() 
    {
        buttonText.text = !isTurnedOn ? "ON" : "OFF"; 
        buttonText.color = !isTurnedOn ? Color.black : Color.white;
        isTurnedOn = !isTurnedOn;
        ButtonColorTransition();
    }
    private void InitializeButtonState()
    {
        buttonText.text = isTurnedOn ? "ON" : "OFF";
        buttonText.color = isTurnedOn ? Color.black : Color.white;
        ButtonColorTransition();
    }

    private byte targetColorAlpha; 
    private readonly float duration = 0.09f;
    private void ButtonColorTransition()
    {
        targetColorAlpha = (byte)(isTurnedOn ? 1 : 0);
        buttonImage.CrossFadeColor(new Color(1, 1, 1, targetColorAlpha), duration, true, true);
    }

    public bool GetSwitchState() => isTurnedOn;
    public object CaptureSate() => new SaveData { isturnedon = this.isTurnedOn };

    public void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        isTurnedOn = savedData.isturnedon;
        InitializeButtonState();
    }

    [Serializable]
    struct SaveData 
    {
        public bool isturnedon; 
    }
}
