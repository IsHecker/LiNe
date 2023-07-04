using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform ZRotation;
    [SerializeField] private Transform XMovement;
    [SerializeField] private Transform YMovement;

    private Vector3 zrotation;
    private Vector3 xOffset;
    private Vector3 yOffset;

    private Camera mainCamera;
    [HideInInspector] public Vector3 XOffset { get => xOffset; set => xOffset = Vector2.right * value; }
    [HideInInspector] public Vector3 YOffset { get => yOffset; set => yOffset = Vector2.up * value; }
    public Vector3 Angle { get => zrotation; set => zrotation = new Vector3(0, 0, value.z); }
    public (float target, float speed) Size { get; set; }
    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        xOffset = XMovement.localPosition;
    }
    
    private void Update()
    {
        XMovement.localPosition = xOffset;
        YMovement.localPosition = yOffset;
        ZRotation.localRotation = Quaternion.Euler(zrotation);
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, Size.target, Size.speed * Time.deltaTime);
    }
}
