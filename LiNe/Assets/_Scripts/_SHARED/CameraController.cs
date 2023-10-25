using EZCameraShake;
using System;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private Vector3 positionOffset;

    [SerializeField] private Transform ZRotation;

    [Header("Auto Follow")]
    [SerializeField] private Transform target;
    [SerializeField] private bool autoFollow = false;
    [SerializeField] private bool XFollow = false;
    [SerializeField] private bool YFollow = false;

    private Transform mytransform;

    private float _cameraWidth, _cameraHeight;

    private Vector3 cameraPosition;
    private Vector3 cameraTarget;
    private Vector3 autoCameraTarget;

    private Transform cameraTransform;

    private Camera _camera;


    #region Properties

    public Camera Camera => _camera;

    public Transform Target { get => target; set { if (value != null) target = value; } }

    public Transform CameraTransform => cameraTransform;

    public Vector3 PositionOffset { get => positionOffset; set => positionOffset = value; }

    public Transform Angle { get => ZRotation; set => ZRotation.localRotation = Quaternion.Euler(0, 0, value.rotation.z); }

    #endregion


    private void Start()
    {
        mytransform = GetComponent<Transform>();
        _camera = Helpers.Camera;
        cameraTransform = Helpers.Camera.transform;

        _cameraWidth = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        _cameraHeight = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
    }

    private void LateUpdate()
    {
        AutoFollow();
    }

    #region Follow Functions

    private void AutoFollow()
    {
        if (!autoFollow || !target) return;

        FollowDirection();

        SmoothFollow(autoCameraTarget);

        void FollowDirection()
        {
            if (XFollow)
                autoCameraTarget.Set(target.position.x, autoCameraTarget.y, autoCameraTarget.z);
            if (YFollow)
                autoCameraTarget.Set(autoCameraTarget.x, target.position.y, autoCameraTarget.z);
        }
    }

    public void SmoothFollow(Vector3 target)
    {
        target += positionOffset;
        cameraPosition.Set(target.x, target.y, mytransform.position.z);
        Vector3 smooth = Vector3.LerpUnclamped(mytransform.position, cameraPosition, followSpeed * Time.deltaTime);
        (smooth.x, smooth.y) = ((float)Math.Round(smooth.x, 3), (float)Math.Round(smooth.y, 3));

        if (CameraShaker.Instance != null)
            CameraShaker.Instance.RestPositionOffset = smooth;
        else
            mytransform.position = smooth;
    }

    public void Follow(Vector3 target)
    {
        target += positionOffset;
        cameraTarget.Set(target.x, target.y, mytransform.position.z);
        cameraPosition = cameraTarget;

        if (CameraShaker.Instance != null)
            CameraShaker.Instance.RestPositionOffset = cameraPosition;
        else
            mytransform.position = cameraPosition;
    }

    public void FollowX(float x)
    {
        cameraPosition = Vector3.right * x;
        Follow(cameraPosition);
    }

    public void FollowY(float y)
    {
        cameraPosition = Vector3.up * y;
        SmoothFollow(cameraPosition);
    }

    #endregion

    #region Simple Tweening

    public LTDescr Move(Vector3 to, float time) => LeanTween.moveLocal(_camera.gameObject, to, time);

    public LTDescr MoveX(float to, float time) => LeanTween.moveLocalX(_camera.gameObject, to, time);

    public LTDescr MoveY(float to, float time) => LeanTween.moveLocalY(_camera.gameObject, to, time);

    public LTDescr Size(float to, float time) => LeanTween.value(_camera.orthographicSize, to, time)
        .setIgnoreTimeScale(true).setOnUpdate((value) 
        => { _camera.orthographicSize = value; });

    public LTDescr RotateZ(float to, float time) => LeanTween.rotateZ(_camera.gameObject, to, time);

    #endregion

    #region Other Functions

    public void SetXFollow(bool follow) => this.XFollow = follow;

    public void SetYFollow(bool follow) => this.YFollow = follow;

    public Vector3 GetPosition() => mytransform.position;

    public float CameraWidth => _cameraWidth;

    public float CameraHeight => _cameraHeight;

    #endregion
}
