using Assets.MAIN_INTERFACES.Shop_TAB;
using Save_System;
using UnityEngine;
using static Helpers;

public class ShopUIEvents : Singleton<ShopUIEvents>
{
    [SerializeField] private Animator animator;
    [SerializeField] private ItemSelector colorSelector;
    [SerializeField] private TMPro.TMP_Text moneyDisplayText;

    private IEquipableItem CurrentItem => GetPressedUI().GetComponent<IEquipableItem>();
    private CameraControl cameraTweak;

    public override void Awake()
    {
        base.Awake();
        UpdateMoneyDisplay();
        cameraTweak = Helpers.Camera.GetComponent<CameraControl>();
    }
    //private void Start() => gameObject.SetActive(false);
    private void UpdateMoneyDisplay() => moneyDisplayText.text = $"{MoneySystem .Money}$";
    public void UpdateColorSelector(Transform target) => colorSelector.PointTo(target);
    public void UpdatePayState(int amount = 0, string state = "EnoughMoney")
    {
        moneyDisplayText.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = $"-{amount}$";
        moneyDisplayText.GetComponent<Animator>().SetTrigger(state);
        UpdateMoneyDisplay();
    }
    public void Equip() => CurrentItem.EquipItem();

    public void ToMainMenu() { SaveLoadSystem.Instance.Save(); MoneySystem.SaveMoney(); ToMenuAnimation();}

    private void ToMenuAnimation() //Simple Animation for preparing shop
    {
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        float target = 1;
        float time = 1.3f;
        Vector3 targetPosition = Vector3.zero;
        LeanTween.value(cameraSize, cameraSize - target, time).setEaseInOutBack().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraTweak.XSlider.gameObject, targetPosition.x, time).setEaseInOutBack();
        LeanTween.moveLocalY(cameraTweak.YSlider.gameObject, targetPosition.y, time).setEaseInOutBack();
        LeanTween.rotateZ(cameraTweak.Angle.gameObject, 0, time).setEaseInOutBack();
    }
    #region implementing money check but didn't work
    //public void Buy()
    //{
    //    if (!IsEnoughMoney()) { UpdatePayState(state: "NotEnoughMoney"); return; }
    //    MoneySystem.Money -= locker.PriceToUnlock;
    //    UpdatePayState(locker.PriceToUnlock);
    //    locker.Unlock();
    //}
    //private bool IsEnoughMoney() => MoneySystem.Money >= locker.PriceToUnlock;
    #endregion
}
