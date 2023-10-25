using Save_System;
using System;
using System.Collections;
using UnityEngine;

public class TutorialHandler : MonoBehaviour, ISaveable
{
    [SerializeField] private GameObject tapToContinueGO;

    private Animator animator;

    private float lastPressTime = float.PositiveInfinity;

    private bool isAbleToSkip = false;
    private bool tutorialIsRead = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Prepare());
    }

    private void Update()
    {
        if (tutorialIsRead) gameObject.SetActive(false);

        if (Input.GetMouseButtonDown(0) && isAbleToSkip) 
        { 
            animator.Play("TutorialUI Inversed");
            lastPressTime = Time.time; 
            isAbleToSkip = false;
        }

        if (Time.time >= lastPressTime + 1f) 
        {
            tutorialIsRead = true;
            SaveLoadSystem.Instance.Save();
            gameObject.SetActive(false); 
        }
    }

    private IEnumerator Prepare()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        animator.enabled = true;

        yield return new WaitForSecondsRealtime(3f);

        tapToContinueGO.SetActive(true);
        isAbleToSkip = true;
    }

    public object CaptureSate()
    {
        return new SaveData { tutorialIsRead = this.tutorialIsRead };
    }

    public void RestoreState(object state)
    {
        tutorialIsRead = ((SaveData)state).tutorialIsRead;
    }

    [Serializable]
    struct SaveData
    {
        public bool tutorialIsRead;
    }
}
