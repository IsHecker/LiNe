using Assets.MAIN_INTERFACES.Shop_TAB;
using Save_System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopUIEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private ItemSelector colorSelector;
    [SerializeField] private TMPro.TMP_Text moneyDisplayText;
    private IEquipableItem currentItem => EventSystem.current.currentSelectedGameObject.GetComponent<IEquipableItem>();
    private CameraControl cameraTweak;
    public static ShopUIEvents Instance { get; private set; }

    private void Awake() 
    {
        Instance = Instance == null ? this : Instance;
        UpdateMoneyDisplay();
        cameraTweak = Camera.main.GetComponent<CameraControl>();
    }

    private void UpdateMoneyDisplay() => moneyDisplayText.text = $"{MoneySystem .Money}$";
    public void UpdateColorSelector(Transform target) => colorSelector.PointTo(target);
    public void UpdatePayState(int amount = 0, string state = "EnoughMoney")
    {
        moneyDisplayText.transform.GetChild(0).GetComponent<TMPro.TMP_Text>().text = $"-{amount}$";
        moneyDisplayText.GetComponent<Animator>().SetTrigger(state);
        UpdateMoneyDisplay();
    }
    public void Equip() => currentItem.EquipItem();


    const int Speed = 4;
    public void ToMainMenu() { SaveLoadSystem.Instance.Save(); MoneySystem.SaveMoney(); ToMenuAnimation(); animator.SetBool("Shop", false); }
    //private void ToMenuAnimation() //Simple Animation for preparing shop
    //{
    //    Vector3 offset = new (cameraTweak.XOffset.x, cameraTweak.YOffset.y, cameraTweak.Angle.z);

    //    StartCoroutine(CameraAnimation());
    //    IEnumerator CameraAnimation()
    //    {
    //        while (true)
    //        {
    //            if (offset.z > -0.1f) yield break;

    //            offset.x = Mathf.LerpUnclamped(offset.x, 0, Speed * Time.deltaTime);
    //            offset.y = Mathf.LerpUnclamped(offset.y, 0, Speed * Time.deltaTime);
    //            offset.z = Mathf.LerpUnclamped(offset.z, 0, Speed * Time.deltaTime);

    //            cameraTweak.Size = (5, 3);
    //            cameraTweak.XOffset = offset;
    //            cameraTweak.YOffset = offset;
    //            cameraTweak.Angle = offset;

    //            yield return new WaitForSeconds(0.001f);
    //        }
    //    }
    //}
    private void ToMenuAnimation() //Simple Animation for preparing shop
    {
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        float targetSize = 5;
        float time = 1.3f;
        Vector3 targetPosition = Vector3.zero;
        LeanTween.value(cameraSize, targetSize, time).setEaseInOutBack().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
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
