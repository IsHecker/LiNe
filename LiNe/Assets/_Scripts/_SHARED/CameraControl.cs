using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform ZRotation;
    [SerializeField] private Transform XMovment;
    [SerializeField] private Transform YMovment;
    [HideInInspector] public Camera MainCamera;

    private Vector3 zrotation;
    private Vector3 xOffset;
    private Vector3 yOffset;

    public Transform XSlider { get => XMovment; set => XMovment.position = Vector2.right * value.position; }
    public Transform YSlider { get => YMovment; set => YMovment.position = Vector2.up * value.position; }
    public Transform Angle { get => ZRotation; set => ZRotation.localRotation = Quaternion.Euler(0,0,value.position.z) ; }
    public (float target, float speed) Size { get; set; }
    private void Awake()
    {
        MainCamera = GetComponent<Camera>();

        ZRotation = ZRotation.transform;
        XMovment = XMovment.transform;
        YMovment = YMovment.transform;
        xOffset = XMovment.localPosition;
    }

    private void Update()
    {
        //XMovement.localPosition = xOffset;
        //YMovement.localPosition = yOffset;
        //ZRotation.localRotation = Quaternion.Euler(zrotation);
        //mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, Size.target, Size.speed * Time.deltaTime);
    }

}
