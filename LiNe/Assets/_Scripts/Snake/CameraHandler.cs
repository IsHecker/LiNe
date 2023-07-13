using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    public Transform player;
    private Camera cam;
    private void Start() => cam = GetComponent<Camera>();

    private const float Speed = 1.5f;
    public void IncreaseFOV()
    {
        StartCoroutine(Animation());
        IEnumerator Animation()
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(0.5f);
            float time = 0;
            float currentSize = cam.orthographicSize;
            while (time <= 1) 
            {
                time += Time.unscaledDeltaTime * Speed;
                float value = curve.Evaluate(time);
                cam.orthographicSize = currentSize + value / 2;
                yield return 0;
            }
            Time.timeScale = 1f;
        }
    }
}
