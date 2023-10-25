using UnityEngine;
using static Helpers;
public class SelectModeUIEvents : MoneyBehaviour
{
    [SerializeField] private Fading fader;

    public void ToWaveMode() => fader.FadeTo("Wave");
    public void ToGravityMode() => fader.FadeTo("Gravity");
    public void ToSnakeMode() => fader.FadeTo("Snake");
}
