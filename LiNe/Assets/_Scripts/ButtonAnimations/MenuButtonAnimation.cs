using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.Button;

public class MenuButtonAnimation : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Vector3 targetScale;

    [SerializeField] private float time;


    private GameObject objectToResize;

    private Vector3 startScale;

    private Image bg;

    private TMPro.TMP_Text text;


    private void Start()
    {
        objectToResize = gameObject;
        startScale = objectToResize.transform.localScale;

        bg = transform.GetChild(0).GetComponent<Image>();

        text = transform.GetChild(1).GetComponent<TMPro.TMP_Text>();

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        objectToResize.LeanScale(targetScale, time);
        LeanTween.value(0, 1, time).setOnUpdate(v => bg.color = new Color(255, 255, 255, v));
        LeanTween.value(text.gameObject, Color.white, Color.black, time).setOnUpdate(v => text.color = v);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        objectToResize.LeanScale(startScale, time);
        LeanTween.value(1, 0, time).setOnUpdate(v => bg.color = new Color(255, 255, 255, v));
        LeanTween.value(text.gameObject, Color.black, Color.white, time).setOnUpdate(v => text.color = v);
    }
}
