using EZCameraShake;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayCamera : MonoBehaviour
{
    [SerializeField] private float smoothCamera;
    private Vector3 cameraPosition;
    private Transform mytransform;
    private float _cameraWidth, _cameraHeight;

    private void Start() 
    { 
        _cameraWidth = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
        _cameraHeight = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        mytransform = GetComponent<Transform>(); 
    }

    private Vector3 cameraTarget;
    public void SmoothFollow(Vector3 target)
    {
        cameraTarget.Set(target.x, target.y, mytransform.position.z);
        cameraPosition = cameraTarget;
        Vector3 smooth = Vector3.LerpUnclamped(transform.position, cameraPosition, smoothCamera * Time.fixedDeltaTime);
        (smooth.x, smooth.y) = ((float)Math.Round(smooth.x, 3), (float)Math.Round(smooth.y, 3));
        CameraShaker.Instance.RestPositionOffset = smooth;
    }
    public void Follow(Vector3 target)
    {
        cameraTarget.Set(target.x, target.y, mytransform.position.z);
        cameraPosition = cameraTarget;
        CameraShaker.Instance.RestPositionOffset = cameraPosition;
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
    public Vector3 GetPosition() => mytransform.position;
    public float CameraWidth => _cameraWidth;
    public float CameraHeight => _cameraHeight;
}
