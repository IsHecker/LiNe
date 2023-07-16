using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    public Transform player;
    private Camera _camera;
    CameraTween cameraTween;
    private void Start() { _camera = GetComponent<Camera>(); cameraTween = new CameraTween(_camera); }

    private const float Speed = 1f;
    public void IncreaseFOV()
    {
        float targetSize = _camera.orthographicSize;
        Time.timeScale = 0;
        StartCoroutine(Animation());
        IEnumerator Animation()
        {
            yield return new WaitForSecondsRealtime(0.3f);
            cameraTween.Size(++targetSize, Speed).setEaseInOutBack().setOnComplete(() => Time.timeScale = 1f);
        }
    }
}
