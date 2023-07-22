using Save_System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsUIEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Slider masterSlider, musicSlider, effectSlider;
    private void Start()
    {
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeEffectVolume();
    }
    private SwitchBehaviour pressedButton => EventSystem.current.currentSelectedGameObject.GetComponent<SwitchBehaviour>();
    public void SwitchState(IOptionable option) => option.SwitchState(pressedButton);
    public void ToMainMenu() { animator.SetBool("Settings", false); SaveLoadSystem.Instance.Save(); }
    public void ChangeMasterVolume() => AudioManager.Instance.ChangeMasterVolume(masterSlider.value);
    public void ChangeMusicVolume() => AudioManager.Instance.ChangeMusicSourceVolume(musicSlider.value);
    public void ChangeEffectVolume() => AudioManager.Instance.ChangeEffectSourceVolume(effectSlider.value);
}
