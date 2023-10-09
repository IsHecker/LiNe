using EZCameraShake;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private Transform target;

    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 angleOffset;

    [SerializeField] private float smoothCamera;

    [SerializeField] private bool follow = false;
    [SerializeField] private bool applyOnXFollow = false;
    [SerializeField] private bool applyOnYFollow = false;

    [SerializeField] private Transform ZRotation;
    [SerializeField] private Transform XMovment;
    [SerializeField] private Transform YMovment;

    [HideInInspector] public Camera MainCamera;

    private Transform _myTransfrom;

    private Vector3 cameraTarget;

    public Transform Target { get => target; set { if (value != null) target = value; } }

    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }
    public Vector3 AngleOffset { get => angleOffset; set => angleOffset = value; }

    public Transform XSlider { get => XMovment; set => XMovment.position = Vector2.right * value.localPosition; }
    public Transform YSlider { get => YMovment; set => YMovment.position = Vector2.up * value.position; }
    public Transform Angle { get => ZRotation; set => ZRotation.localRotation = Quaternion.Euler(0, 0, value.rotation.z); }

    protected override void Awake()
    {
        base.Awake();
        MainCamera = Helpers.Camera;
        ZRotation = ZRotation.transform;
        XMovment = XMovment.transform;
        YMovment = YMovment.transform;
        _myTransfrom = GetComponent<Transform>();
    }


    private void LateUpdate()
    {
        if (!follow || target == null) return;
        
        AutoFollow();
    }

    private void AutoFollow()
    {
        FollowDirection();

        _myTransfrom.position = cameraTarget + positionOffset;
    }

    private void FollowDirection()
    {
        if (applyOnXFollow)
            cameraTarget.Set(target.position.x, 0, 0);
        else if (applyOnYFollow)
            cameraTarget.Set(0, target.position.y, 0);
        else
            cameraTarget.Set(target.position.x, target.position.y, 0);
    }

    public void SetFollow(bool follow) => this.follow = follow;

    public void SetXFollow(bool follow) => this.applyOnXFollow = follow;

    public void SetYFollow(bool follow) => this.applyOnYFollow = follow;
}
