using EZCameraShake;
using System;
using UnityEngine;

public class GamePlayCamera : MonoBehaviour
{
    [SerializeField] private float smoothCamera;
    private Vector3 cameraPosition;
    private Transform mytransform;

    private void Start() => mytransform = GetComponent<Transform>();
    public void SmoothFollow(Vector3 target)
    {
        cameraPosition = new Vector3(target.x, target.y, mytransform.position.z);
        Vector3 smooth = Vector3.LerpUnclamped(transform.position, cameraPosition, smoothCamera * Time.fixedDeltaTime);
        (smooth.x, smooth.y) = ((float)Math.Round(smooth.x, 3), (float)Math.Round(smooth.y, 3));
        CameraShaker.Instance.RestPositionOffset = smooth;
    }
    public void Follow(Vector3 target)
    {
        cameraPosition = new Vector3(target.x, target.y, mytransform.position.z);
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
}
