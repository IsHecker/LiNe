using UnityEngine;
using UnityEngine.UI;

namespace Assets.MAIN_INTERFACES.Shop_TAB
{
    public class ColorItem : MonoBehaviour, IEquipableItem
    {
        private TrailRenderer playerTrailColor;

        private void Awake() => playerTrailColor = GameObject.Find("Animated Head").GetComponent<TrailRenderer>();

        public void EquipItem() 
        {
            ScenesData.trailColor = playerTrailColor.material.color = GetComponent<Image>().color;
            ShopUIEvents.Instance.UpdateColorSelector(transform);
        }
    }
}
