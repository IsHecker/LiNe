using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectModeUIEvents : MonoBehaviour
{
    [SerializeField] private Fading fader;
    public void ToMainMenu() => fader.Canvastranstion("Selection Menu_UI");
    public void ToWaveMode() => fader.FadeTo("Wave");
    public void ToGravityMode() => fader.FadeTo("Gravity");
    public void ToSnakeMode() => fader.FadeTo("Snake");
}
