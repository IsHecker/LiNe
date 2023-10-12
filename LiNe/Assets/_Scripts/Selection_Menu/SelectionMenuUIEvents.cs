using UnityEngine;

public class SelectionMenuUIEvents : Singleton<SelectionMenuUIEvents>
{
    public Animator animator;

    [SerializeField] private Fading fader;

    private CameraController cameraController;

    private void OnEnable()
    {
        cameraController = CameraController.Instance;
        if (cameraController == null) print("Fuck");
    }

    public void ToShopAnimation() //Simple Animation for preparing shop
    {
        float cameraSize = cameraController.Camera.orthographicSize;
        float target = 1;
        float time = 1.3f;
        Vector3 targetPosition = new Vector3(2.8f, -1);
        LeanTween.value(cameraSize, cameraSize + target, time).setEaseInOutBack().setOnUpdate((value) => { cameraController.Camera.orthographicSize = value; });
        LeanTween.moveLocalX(cameraController.CameraTransform.gameObject, targetPosition.x, time).setEaseInOutBack();
        LeanTween.moveLocalY(cameraController.CameraTransform.gameObject, targetPosition.y, time).setEaseInOutBack();
        LeanTween.rotateZ(cameraController.Angle.gameObject, -90, time).setEaseInOutBack();
    }
}
