using Assets.MAIN_INTERFACES.Shop_TAB;
using Save_System;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LockerBehaviour : MonoBehaviour, ISaveable, ILocker
{
    [SerializeField] private int priceToUnlock;

    private bool isLocked = true;

    public int PriceToUnlock => priceToUnlock;

    private ShopUIEvents shopUIEvents;

    private void OnEnable()
    {
        shopUIEvents = ShopUIEvents.Instance;
    }

    public void CheckIfCanUnlock()
    {
        if (!ShopUIEvents.Instance.IsEnoughMoney(priceToUnlock)) { ShopUIEvents.Instance.UpdatePayState(state: "NotEnoughMoney"); return; }

        ShopUIEvents.Instance.MakeSureUI.SetActive(true);
        MakeSure.OnApproveEvent(Unlock);
    }

    public void Unlock()
    {
        isLocked = false;
        transform.parent.GetComponent<IEquipableItem>().EquipItem(); //To equip the item immediately once it Unlocks.
        MoneySystem.Money -= PriceToUnlock;
        LockerState(isLocked);
        ShopUIEvents.Instance.UpdatePayState(PriceToUnlock);
    }

    private void LockerState(bool LockState)
    {
       gameObject.SetActive(LockState);
    }

    public object CaptureSate()
    {
        if (!isLocked)
            print(transform.parent.name);
        return new SaveData { isLocked = this.isLocked };
    }

    public void RestoreState(object state)
    {
        SaveData savedData = (SaveData)state;
        isLocked = savedData.isLocked;
        LockerState(isLocked);
    }

    [Serializable]
    struct SaveData
    {
        public bool isLocked;
    }
}
