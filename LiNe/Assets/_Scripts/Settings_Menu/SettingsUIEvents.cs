using Save_System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsUIEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private SwitchBehaviour pressedButton => EventSystem.current.currentSelectedGameObject.GetComponent<SwitchBehaviour>();
    public void SwitchState(IOptionable option) => option.SwitchState(pressedButton);
    public void ToMainMenu() { animator.SetBool("Settings", false); SaveLoadSystem.Instance.Save(); }
}
