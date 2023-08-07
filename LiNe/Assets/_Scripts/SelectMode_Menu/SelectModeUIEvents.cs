using UnityEngine;
using static Helpers;
public class SelectModeUIEvents : MoneyBehaviour
{
    [SerializeField] private Fading fader;
    [SerializeField] private Animator animator;

    public void ToWaveMode() => fader.FadeTo("Wave");
    public void ToGravityMode() => fader.FadeTo("Gravity");
    public void ToSnakeMode() => fader.FadeTo("Snake");
}
