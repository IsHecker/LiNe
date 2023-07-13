using System.Collections;
using UnityEngine;

public class SelectionMenuUIEvents : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Fading fader;
    private CameraControl cameraTweak;
    private void Awake() => cameraTweak = Camera.main.GetComponent<CameraControl>();
    public void ToSettings() => animator.SetBool("Settings", true);
    public void ToShop() { ToShopAnimation(); animator.SetBool("Shop", true); }
    public void ToSelectModeMenu() => fader.Canvastranstion("SelectMode_UI");
    private void ToShopAnimation() //Simple Animation for preparing shop
    {
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        float targetSize = 6;
        float time = 1.3f;
        Vector3 targetPosition = new Vector3(2.8f, -1);
        LeanTween.value(cameraSize, targetSize, time).setEaseInOutBack().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraTweak.XSlider.gameObject, targetPosition.x, time).setEaseInOutBack();
        LeanTween.moveLocalY(cameraTweak.YSlider.gameObject, targetPosition.y, time).setEaseInOutBack();
        LeanTween.rotateZ(cameraTweak.Angle.gameObject, -90, time).setEaseInOutBack();
    }
}
