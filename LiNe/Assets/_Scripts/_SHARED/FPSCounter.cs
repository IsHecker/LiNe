﻿using System;
using UnityEngine;

public class FPSCounter : IOptionable
{
    [SerializeField] private ScenesData scenesData;

    private TMPro.TMP_Text frames;
    private float fpsCounter;


    private void OnEnable()
    {
        frames = GetComponent<TMPro.TMP_Text>();
        InvokeRepeating(nameof(FPS), 0, 1f);
    }

    private void FPS()
    {
        fpsCounter = 1 / Time.deltaTime;
        frames.text = "FPS: " + fpsCounter.ToString("0");
    }


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
