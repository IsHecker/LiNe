using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : IOptionable
{
    private TMPro.TMP_Text frames;
    private float fpsCounter;
    // Start is called before the first frame update
    private void OnEnable()
    {
        frames = GetComponent<TMPro.TMP_Text>();
        InvokeRepeating(nameof(FPS), 0, 1.5f);
    }
    private void FPS()
    {
        fpsCounter = 1 / Time.deltaTime;
        frames.text = "FPS: " + fpsCounter.ToString("0");
    }
    [SerializeField] private ScenesData scenesData;
    public override void SwitchState(SwitchBehaviour switchValue) 
    {
        frames.enabled = switchValue.GetSwitchState();
        scenesData.fpsActiveState = frames.IsActive(); 
    }
    public override object CaptureSate() => new SaveData { turnedOn = frames.IsActive() };

    public override void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        frames.enabled = savedData.turnedOn;
    }

    [Serializable]
    struct SaveData
    {
        public bool turnedOn;
    }
}
