using Save_System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsUIEvents : MonoBehaviour
{
    [SerializeField] private Slider masterSlider, musicSlider, effectSlider;


    private void Start()
    {
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeEffectVolume();
        Helpers.Invoke(() => gameObject.SetActive(false), 0.1f);
    }

    private SwitchBehaviour PressedButton => EventSystem.current.currentSelectedGameObject.GetComponent<SwitchBehaviour>();

    public void SaveSettings() { SaveLoadSystem.Instance.Save(); }

    public void SwitchState(IOptionable option) => option.SwitchState(PressedButton);

    public void ChangeMasterVolume() => AudioManager.Instance.ChangeMasterVolume(masterSlider.value);

    public void ChangeMusicVolume() => AudioManager.Instance.ChangeMusicSourceVolume(musicSlider.value);

    public void ChangeEffectVolume() => AudioManager.Instance.ChangeEffectSourceVolume(effectSlider.value);
}
