using System.Collections;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    private Animator animator;
    private bool isAbleToSkip = false;
    private float lastPressTime = float.PositiveInfinity;
    [SerializeField] private GameObject tapToContinueGO;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(prepare());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAbleToSkip) { animator.Play("TutorialUI Inversed"); lastPressTime = Time.time; isAbleToSkip = false; }
        if (Time.time >= lastPressTime + 1f) gameObject.SetActive(false);
    }
    private IEnumerator prepare()
    {
        yield return new WaitForSecondsRealtime(1f);
        animator.enabled = true;
        yield return new WaitForSecondsRealtime(3f);
        tapToContinueGO.SetActive(true);
        isAbleToSkip = true;
    }
}
