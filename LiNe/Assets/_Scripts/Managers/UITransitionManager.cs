using UnityEngine;
using static Helpers;

public class UITransitionManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private GameObject selectionMenu_UI, selectMode_UI, shop_UI, settings_UI;


    public void ToSelectionMenu(string name)
    {
        GetPressedUI().transform.parent.gameObject.TransitionToUI(selectionMenu_UI, animator, name, false, 0.5f);
    }

    public void ShopToMenu()
    {
        shop_UI.TransitionToUI(selectionMenu_UI, animator, "shop", false, 1f);
        ShopUIEvents.Instance.ToMainMenu();
    }

    public void ToSelectMode(string animationName) => selectionMenu_UI.TransitionToUI(selectMode_UI, animator, animationName, true, 0.5f);

    public void ToSettings(string animationName) => selectionMenu_UI.TransitionToUI(settings_UI, animator, animationName, true, 0.5f);

    public void ToShop(string animationName)
    {
        selectionMenu_UI.TransitionToUI(shop_UI, animator, animationName, true, 0.5f);
        SelectionMenuUIEvents.Instance.ToShopAnimation();
    }

    public void Exit() => Application.Quit();
}
