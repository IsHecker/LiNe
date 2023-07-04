using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextAnimator : MonoBehaviour
{
    public List<Animator> anims;
    public string triggername;
    public float timebetweenletters;
    public float dealyafteranim;

    // Start is called before the first frame update
    private void OnEnable()
    {
        anims = new List<Animator>(GetComponentsInChildren<Animator>());
        StartCoroutine(loop());
    }
    IEnumerator loop()
    {
        foreach (Animator anim in anims)
        {
            yield return new WaitForSeconds(timebetweenletters);
            anim.SetTrigger(triggername);
        }
        yield return new WaitForSeconds(dealyafteranim);
    }
}
