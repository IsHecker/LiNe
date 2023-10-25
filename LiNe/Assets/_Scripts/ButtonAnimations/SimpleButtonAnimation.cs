using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SimpleButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private GameObject objectToResize;

    [SerializeField] private Vector3 targetScale = new Vector3(0.8f, 0.8f, 1f);

    [SerializeField] private float time = 0.05f;

    [SerializeField] private bool callEvent = false;

    private Vector3 startScale;

    private Button button;


    private void Start()
    {
        button = GetComponent<Button>();
        startScale = objectToResize.transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectToResize.LeanScale(targetScale, time).setIgnoreTimeScale(true);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        objectToResize.LeanScale(startScale, time).setIgnoreTimeScale(true);

        if (callEvent)
            button.onClick?.Invoke();
    }
}
