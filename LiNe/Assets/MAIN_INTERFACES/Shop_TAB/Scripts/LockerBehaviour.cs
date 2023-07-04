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
    public void Unlock()
    {
        if (!IsEnoughMoney()) { ShopUIEvents.Instance.UpdatePayState(state: "NotEnoughMoney"); return; }
        isLocked = false;
        transform.parent.GetComponent<IEquipableItem>().EquipItem(); //To equip the item immediately once it Unlocks.
        ShopUIEvents.Instance.UpdatePayState(PriceToUnlock);
        //MoneySystem.Money -= PriceToUnlock;
        LockerState(isLocked);
    }
    private void LockerState(bool LockState)
    {
        GetComponent<Image>().enabled = LockState;
        GetComponent<Button>().enabled = LockState;
        transform.GetChild(0).gameObject.SetActive(LockState);
    }
    public bool IsEnoughMoney() => MoneySystem.Money >= priceToUnlock;

    public object CaptureSate() => new SaveData { isLocked = this.isLocked };

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
