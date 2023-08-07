using UnityEngine;

public class SelectionMenuUIEvents : Singleton<SelectionMenuUIEvents>
{
    public Animator animator;
    [SerializeField] private Fading fader;

    private CameraControl cameraTweak;
    public override void Awake()
    {
        base.Awake();
        cameraTweak = Helpers.Camera.GetComponent<CameraControl>();
    }

    public void ToShopAnimation() //Simple Animation for preparing shop
    {
        float cameraSize = cameraTweak.MainCamera.orthographicSize;
        float target = 1;
        float time = 1.3f;
        Vector3 targetPosition = new Vector3(2.8f, -1);
        LeanTween.value(cameraSize, cameraSize + target, time).setEaseInOutBack().setOnUpdate((value) => { cameraTweak.MainCamera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraTweak.XSlider.gameObject, targetPosition.x, time).setEaseInOutBack();
        LeanTween.moveLocalY(cameraTweak.YSlider.gameObject, targetPosition.y, time).setEaseInOutBack();
        LeanTween.rotateZ(cameraTweak.Angle.gameObject, -90, time).setEaseInOutBack();
    }
}
