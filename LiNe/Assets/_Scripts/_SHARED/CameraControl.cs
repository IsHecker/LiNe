using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Transform ZRotation;
    [SerializeField] private Transform XMovment;
    [SerializeField] private Transform YMovment;
    [HideInInspector] public Camera MainCamera;
    public Transform XSlider { get => XMovment; set => XMovment.position = Vector2.right * value.localPosition; }
    public Transform YSlider { get => YMovment; set => YMovment.position = Vector2.up * value.position; }
    public Transform Angle { get => ZRotation; set => ZRotation.localRotation = Quaternion.Euler(0,0,value.rotation.z) ; }
    private void Awake()
    {
        MainCamera = GetComponent<Camera>();
        ZRotation = ZRotation.transform;
        XMovment = XMovment.transform;
        YMovment = YMovment.transform;
    }

}
