using Assets.MAIN_INTERFACES.Shop_TAB;
using Save_System;
using UnityEngine;
using static Helpers;

public class ShopUIEvents : Singleton<ShopUIEvents>
{
    [SerializeField] private Animator animator;

    [SerializeField] private ItemSelector colorSelector;

    [SerializeField] private TMPro.TMP_Text moneyDisplayText;

    public GameObject MakeSureUI;


    private IEquipableItem CurrentItem => GetPressedUI().GetComponent<IEquipableItem>();

    private void OnEnable()
    {
        UpdateMoneyDisplay();
    }

    private void Start() => Helpers.Invoke(() => gameObject.SetActive(false), 0.1f);

    private void UpdateMoneyDisplay() => moneyDisplayText.text = $"{MoneySystem.Money}$";

    public void UpdateColorSelector(Transform target) => colorSelector.PointTo(target);

    public void UpdatePayState(int amount = 0, string state = "EnoughMoney")
    {
        moneyDisplayText.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = $"-{amount}$";
        moneyDisplayText.GetComponent<Animator>().SetTrigger(state);
        UpdateMoneyDisplay();
    }

    public void Equip() => CurrentItem.EquipItem();

    public bool IsEnoughMoney(int itemPrice) => MoneySystem.Money >= itemPrice;


    #region Animation Functions

    public void ToMainMenu() 
    {
        SaveLoadSystem.Instance.Save();
        MoneySystem.SaveMoney();
        ToMenuAnimation();
    }

    private void ToMenuAnimation() //Simple Animation for preparing Menu
    {
        CameraController cameraController = CameraController.Instance;
        float cameraSize = cameraController.Camera.orthographicSize;
        float target = 1;
        float time = 1.3f;
        Vector3 targetPosition = Vector3.zero;
        LeanTween.value(cameraSize, cameraSize - target, time).setEaseInOutBack().setOnUpdate((value) => { cameraController.Camera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraController.CameraTransform.gameObject, targetPosition.x, time).setEaseInOutBack();
        LeanTween.moveLocalY(cameraController.CameraTransform.gameObject, targetPosition.y, time).setEaseInOutBack();
        LeanTween.rotateZ(cameraController.Angle.gameObject, 0, time).setEaseInOutBack();
    }

    #endregion

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
