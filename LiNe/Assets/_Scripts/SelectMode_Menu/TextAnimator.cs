using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    private List<Animator> anims;

    public string triggername;
    public float timebetweenletters;
    public float dealyafteranim;
    // Start is called before the first frame update
    private void Awake()
    {
        anims = new List<Animator>(GetComponentsInChildren<Animator>());
    }

    private void OnEnable()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        foreach (Animator anim in anims)
        {
            yield return new WaitForSeconds(timebetweenletters);
            anim.SetTrigger(triggername);
        }
        yield return new WaitForSeconds(dealyafteranim);
    }
    private void OnDisable()
    {
        StopCoroutine(Loop());
    }
}
